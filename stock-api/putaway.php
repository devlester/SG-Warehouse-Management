<?php
header("Content-Type: text/plain");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo "ERROR|DB";
    exit;
}

$store = isset($_GET['store_location']) ? $_GET['store_location'] : "";


// 👉 CASE 1: NO STORE → RETURN STORE LIST
if ($store == "") {

    $sql = "SELECT DISTINCT po.store_location, pi.pick_no 
            FROM picked_items pi
            JOIN picking_orders po ON pi.pick_no = po.pick_no";

    $result = mysqli_query($conn, $sql);

    if ($result && mysqli_num_rows($result) > 0) {

        while ($row = mysqli_fetch_assoc($result)) {
            echo "STORE|" . $row['store_location'] . "|" . $row['pick_no'] . "\n";
        }

    } else {
        echo "NO_DATA";
    }


// 👉 CASE 2: WITH STORE → RETURN PRODUCTS (AVAILABLE TO BOX)
} else {

    $sql = "SELECT 
                pi.pick_no,
                po.store_location,
                pl.product_location,
                pr.product_style,
                pi.product_code,

                IFNULL(SUM(pi.picked_qty), 0) AS picked_qty,
                IFNULL(pa.total_boxed, 0) AS boxed_qty,

                (
                    IFNULL(SUM(pi.picked_qty), 0) - IFNULL(pa.total_boxed, 0)
                ) AS remaining_to_box

            FROM picked_items pi

            JOIN picking_orders po 
            ON pi.pick_no = po.pick_no

            JOIN products pr 
            ON pi.product_code = pr.product_code

            JOIN product_locations pl 
            ON pr.product_style = pl.product_style

            -- ✅ aggregate put-away FIRST (avoid duplication)
            LEFT JOIN (
                SELECT pick_no, product_code, SUM(put_qty) AS total_boxed
                FROM `put-away`
                GROUP BY pick_no, product_code
            ) pa
            ON pi.pick_no = pa.pick_no
            AND pi.product_code = pa.product_code

            WHERE po.store_location = '$store'

            GROUP BY 
                pi.pick_no,
                po.store_location,
                pl.product_location,
                pr.product_style,
                pi.product_code

            HAVING remaining_to_box > 0

            ORDER BY pl.product_location ASC";

    $result = mysqli_query($conn, $sql);

    if ($result && mysqli_num_rows($result) > 0) {

        while ($row = mysqli_fetch_assoc($result)) {

            echo "PRODUCT|" .
                 $row['product_location'] . "|" .
                 $row['product_style'] . "|" .
                 $row['remaining_to_box'] . "\n";
        }

    } else {
        echo "NO_DATA";
    }

}
?>