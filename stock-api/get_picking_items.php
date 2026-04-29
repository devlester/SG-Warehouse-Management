<?php
header("Content-Type: application/json");

$conn     = mysqli_connect("localhost", "root", "", "stocktake");
$location = $_GET['store_location'];
$pickno   = $_GET['pick_no'];

$totalSql = "SELECT
    (SELECT SUM(qty_to_pick)
     FROM picking_items
     WHERE store_location = '$location'
     AND pick_no = '$pickno') AS total_qty_to_pick,

    (SELECT IFNULL(SUM(picked_qty), 0)
     FROM picked_items
     WHERE pick_no = '$pickno') AS total_picked,

    (SELECT IFNULL(SUM(put_qty), 0)
     FROM `put-away`
     WHERE pick_no = '$pickno') AS total_boxed";

$totalRow = $conn->query($totalSql)->fetch_assoc();

$sql = "SELECT
        p.store_location,
        p.product_location,
        pr.product_style,
        p.product_code,
        p.qty_to_pick,
        IFNULL(pi.total_picked, 0) AS picked_qty,
        IFNULL(pa.total_boxed, 0)  AS boxed_qty,
        (p.qty_to_pick - IFNULL(pi.total_picked, 0))                   AS remaining_to_pick,
        (IFNULL(pi.total_picked, 0) - IFNULL(pa.total_boxed, 0))       AS remaining_to_box,
        CASE
            WHEN (
                (SELECT IFNULL(SUM(qty_to_pick), 0) FROM picking_items WHERE pick_no = p.pick_no)
                - (SELECT IFNULL(SUM(picked_qty), 0) FROM picked_items  WHERE pick_no = p.pick_no)
            ) <= 0 THEN 'COMPLETED'
            ELSE 'PENDING'
        END AS status
        FROM picking_items p
        LEFT JOIN products pr ON p.product_code = pr.product_code
        LEFT JOIN (
            SELECT pick_no, product_code, SUM(picked_qty) AS total_picked
            FROM picked_items
            GROUP BY pick_no, product_code
        ) pi ON p.product_code = pi.product_code AND p.pick_no = pi.pick_no
        LEFT JOIN (
            SELECT pick_no, product_code, SUM(put_qty) AS total_boxed
            FROM `put-away`
            GROUP BY pick_no, product_code
        ) pa ON p.product_code = pa.product_code AND p.pick_no = pa.pick_no
        WHERE p.store_location = '$location'
        AND   p.pick_no        = '$pickno'
        ORDER BY p.product_location ASC";

$result = $conn->query($sql);
$items  = [];

while ($row = $result->fetch_assoc()) {
    $items[] = [
        "store"             => $row['store_location'],
        "product_location"  => $row['product_location'],
        "product_style"     => $row['product_style'],
        "product_code"      => $row['product_code'],
        "qty_to_pick"       => (int)$row['qty_to_pick'],
        "picked_qty"        => (int)$row['picked_qty'],
        "boxed_qty"         => (int)$row['boxed_qty'],
        "remaining_to_pick" => (int)$row['remaining_to_pick'],
        "remaining_to_box"  => (int)$row['remaining_to_box'],
        "status"            => $row['status']
    ];
}

echo json_encode([
    "total_to_pick" => (int)$totalRow['total_qty_to_pick'],
    "total_picked"  => (int)$totalRow['total_picked'],
    "total_boxed"   => (int)$totalRow['total_boxed'],
    "items"         => $items
]);
?>
