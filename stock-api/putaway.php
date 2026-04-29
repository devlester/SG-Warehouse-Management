<?php
header("Content-Type: application/json");

$conn  = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$store = isset($_GET['store_location']) ? $_GET['store_location'] : "";

// ── no store → return store list ─────────────────────────────
if ($store == "") {

    $result = mysqli_query($conn,
        "SELECT DISTINCT po.store_location, pi.pick_no
         FROM picked_items pi
         JOIN picking_orders po ON pi.pick_no = po.pick_no");

    $stores = [];
    if ($result && mysqli_num_rows($result) > 0) {
        while ($row = mysqli_fetch_assoc($result)) {
            $stores[] = ["store" => $row['store_location'], "pick_no" => $row['pick_no']];
        }
    }
    echo json_encode(["stores" => $stores]);

// ── with store → return products available to box ────────────
} else {

    $result = mysqli_query($conn,
        "SELECT
            pi.pick_no,
            po.store_location,
            pl.product_location,
            pr.product_style,
            pi.product_code,
            IFNULL(SUM(pi.picked_qty), 0)    AS picked_qty,
            IFNULL(pa.total_boxed, 0)         AS boxed_qty,
            (IFNULL(SUM(pi.picked_qty), 0)
             - IFNULL(pa.total_boxed, 0))     AS remaining_to_box
         FROM picked_items pi
         JOIN picking_orders po  ON pi.pick_no       = po.pick_no
         JOIN products pr        ON pi.product_code  = pr.product_code
         JOIN product_locations pl ON pr.product_style = pl.product_style
         LEFT JOIN (
             SELECT pick_no, product_code, SUM(put_qty) AS total_boxed
             FROM `put-away`
             GROUP BY pick_no, product_code
         ) pa ON pi.pick_no = pa.pick_no AND pi.product_code = pa.product_code
         WHERE po.store_location = '$store'
         GROUP BY pi.pick_no, po.store_location, pl.product_location,
                  pr.product_style, pi.product_code
         HAVING remaining_to_box > 0
         ORDER BY pl.product_location ASC");

    $products = [];
    if ($result && mysqli_num_rows($result) > 0) {
        while ($row = mysqli_fetch_assoc($result)) {
            $products[] = [
                "location"      => $row['product_location'],
                "style"         => $row['product_style'],
                "remaining_qty" => (int)$row['remaining_to_box']
            ];
        }
    }
    echo json_encode(["products" => $products]);
}
?>
