<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$pickno = $_GET['pick_no']        ?? '';
$store  = $_GET['store_location'] ?? '';

if ($pickno == '') {
    echo json_encode(["status" => "error", "message" => "INVALID_INPUT"]);
    exit;
}

// ── totals ───────────────────────────────────────────────────
$totals = $conn->query("
    SELECT
        (SELECT IFNULL(SUM(qty_to_pick), 0) FROM picking_items WHERE pick_no = '$pickno') AS total_to_pick,
        (SELECT IFNULL(SUM(picked_qty),  0) FROM picked_items  WHERE pick_no = '$pickno') AS total_picked,
        (SELECT IFNULL(SUM(put_qty),     0) FROM `put-away`    WHERE pick_no = '$pickno') AS total_boxed
")->fetch_assoc();

// ── line items with discrepancy ───────────────────────────────
$itemsResult = $conn->query("
    SELECT
        pi.product_location,
        pr.product_style,
        pr.product_name,
        pi.qty_to_pick,
        IFNULL(pk.total_picked, 0)                          AS qty_picked,
        IFNULL(pa.total_boxed,  0)                          AS qty_boxed,
        (pi.qty_to_pick - IFNULL(pk.total_picked, 0))       AS discrepancy
    FROM picking_items pi
    JOIN products pr ON pi.product_code = pr.product_code
    LEFT JOIN (
        SELECT pick_no, product_code, SUM(picked_qty) AS total_picked
        FROM picked_items GROUP BY pick_no, product_code
    ) pk ON pi.pick_no = pk.pick_no AND pi.product_code = pk.product_code
    LEFT JOIN (
        SELECT pick_no, product_code, SUM(put_qty) AS total_boxed
        FROM `put-away` GROUP BY pick_no, product_code
    ) pa ON pi.pick_no = pa.pick_no AND pi.product_code = pa.product_code
    WHERE pi.pick_no = '$pickno'
    ORDER BY pi.product_location ASC
");

$items = [];
while ($row = $itemsResult->fetch_assoc()) {
    $items[] = [
        "location"    => $row['product_location'],
        "style"       => $row['product_style'],
        "name"        => $row['product_name'],
        "qty_to_pick" => (int)$row['qty_to_pick'],
        "qty_picked"  => (int)$row['qty_picked'],
        "qty_boxed"   => (int)$row['qty_boxed'],
        "discrepancy" => (int)$row['discrepancy']
    ];
}

// ── box breakdown ─────────────────────────────────────────────
$boxResult = $conn->query("
    SELECT pa.box_no, pr.product_style, pr.product_name,
           SUM(pa.put_qty) AS qty
    FROM `put-away` pa
    JOIN products pr ON pa.product_code = pr.product_code
    WHERE pa.pick_no = '$pickno'
    GROUP BY pa.box_no, pa.product_code
    ORDER BY CAST(pa.box_no AS UNSIGNED), pa.product_code
");

$boxes = [];
while ($row = $boxResult->fetch_assoc()) {
    $boxes[] = [
        "box_no" => $row['box_no'],
        "style"  => $row['product_style'],
        "name"   => $row['product_name'],
        "qty"    => (int)$row['qty']
    ];
}

echo json_encode([
    "status"       => "ok",
    "store"        => $store,
    "pick_no"      => $pickno,
    "printed_at"   => date("d/m/Y H:i"),
    "total_to_pick"=> (int)$totals['total_to_pick'],
    "total_picked" => (int)$totals['total_picked'],
    "total_boxed"  => (int)$totals['total_boxed'],
    "items"        => $items,
    "boxes"        => $boxes
]);
?>
