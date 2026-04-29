<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$location  = $_GET['location'];
$stylecode = $_GET['stylecode'];
$barcode   = $_GET['barcode'];
$stats     = isset($_GET['stats']) ? $_GET['stats'] : "fromMain";

$sql = "SELECT
            p.pick_no,
            p.store_location,
            p.product_location,
            p.product_code,
            p.qty_to_pick,
            IFNULL(pi.total_picked, 0) AS picked_qty,
            IFNULL(pa.total_boxed, 0)  AS putaway_qty,
            pr.product_style,
            pr.product_name
        FROM picking_items AS p
        JOIN products AS pr ON p.product_code = pr.product_code
        LEFT JOIN (
            SELECT pick_no, product_code, SUM(picked_qty) AS total_picked
            FROM picked_items
            GROUP BY pick_no, product_code
        ) pi ON p.pick_no = pi.pick_no AND p.product_code = pi.product_code
        LEFT JOIN (
            SELECT pick_no, product_code, SUM(put_qty) AS total_boxed
            FROM `put-away`
            GROUP BY pick_no, product_code
        ) pa ON p.pick_no = pa.pick_no AND p.product_code = pa.product_code
        WHERE p.product_location = '$location'
        AND   pr.product_style   = '$stylecode'
        AND   p.product_code     = '$barcode'";

$result = mysqli_query($conn, $sql);

if ($result && mysqli_num_rows($result) > 0) {
    $row    = mysqli_fetch_assoc($result);
    $picked = (int)$row['picked_qty'];
    $boxed  = (int)$row['putaway_qty'];

    $final_qty = ($stats == "fromPutaway") ? ($picked - $boxed) : $picked;

    echo json_encode([
        "status"           => "found",
        "product_location" => $row['product_location'],
        "product_code"     => $row['product_code'],
        "qty_to_pick"      => (int)$row['qty_to_pick'],
        "picked_qty"       => $final_qty,
        "putaway_qty"      => $boxed,
        "product_style"    => $row['product_style'],
        "product_name"     => $row['product_name']
    ]);
} else {
    echo json_encode(["status" => "invalid"]);
}
?>
