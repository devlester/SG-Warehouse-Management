<?php
header("Content-Type: text/plain");

$conn = mysqli_connect("localhost", "root", "", "stocktake");

$location = $_GET['store_location'];
$pickno   = $_GET['pick_no'];

# 👉 TOTALS (FIXED LOGIC)
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

$totalResult = $conn->query($totalSql);
$totalRow = $totalResult->fetch_assoc();

$totalToPick = $totalRow['total_qty_to_pick'];
$totalPicked = $totalRow['total_picked'];
$totalBoxed  = $totalRow['total_boxed'];

# 👉 MAIN QUERY
$sql = "SELECT 
        p.store_location,
        p.product_location,
        pr.product_style,
        p.product_code,
        p.qty_to_pick,

        IFNULL(pi.total_picked, 0) AS picked_qty,
        IFNULL(pa.total_boxed, 0) AS boxed_qty,

        (p.qty_to_pick - IFNULL(pi.total_picked, 0)) AS remaining_to_pick,

        (IFNULL(pi.total_picked, 0) - IFNULL(pa.total_boxed, 0)) AS remaining_to_box,

        CASE 
            WHEN (
                (SELECT IFNULL(SUM(qty_to_pick),0) 
                 FROM picking_items 
                 WHERE pick_no = p.pick_no)

                -

                (SELECT IFNULL(SUM(picked_qty),0) 
                 FROM picked_items 
                 WHERE pick_no = p.pick_no)

            ) <= 0 THEN 'COMPLETED'
            ELSE 'PENDING'
        END AS status

        FROM picking_items p

        LEFT JOIN products pr 
        ON p.product_code = pr.product_code

        -- ✅ FIX: aggregated picked_items
        LEFT JOIN (
            SELECT pick_no, product_code, SUM(picked_qty) AS total_picked
            FROM picked_items
            GROUP BY pick_no, product_code
        ) pi
        ON p.product_code = pi.product_code
        AND p.pick_no = pi.pick_no

        -- ✅ FIX: aggregated put-away
        LEFT JOIN (
            SELECT pick_no, product_code, SUM(put_qty) AS total_boxed
            FROM `put-away`
            GROUP BY pick_no, product_code
        ) pa
        ON p.product_code = pa.product_code
        AND p.pick_no = pa.pick_no

        WHERE p.store_location = '$location'
        AND p.pick_no = '$pickno'

        ORDER BY p.product_location ASC";

$result = $conn->query($sql);

while($row = $result->fetch_assoc()){

    echo $row['store_location'] . "|" .
         $totalToPick . "|" .
         $totalPicked . "|" .
         $totalBoxed . "|" .   // 👈 NEW
         $row['product_location'] . "|" .
         $row['product_style'] . "|" .
         $row['remaining_to_pick'] . "|" .
         $row['remaining_to_box'] . "|" .
         $row['status'] . "\n";
}
?>