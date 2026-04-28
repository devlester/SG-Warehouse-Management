<?php
header("Content-Type: text/plain");

$conn = mysqli_connect("localhost", "root", "", "stocktake");

if (!$conn) {
    echo "ERROR|DB";
    exit;
}
$pickno = $_GET['pick_no'] ?? '';
$boxno  = $_GET['box_no'] ?? '';

//$pickno = 'P-08032026-002';

if ($pickno == '') {
    echo "ERROR|INVALID_INPUT";
    exit;
}

# =========================
# 🔥 IF NO BOX → SHOW ALL
# =========================
if ($boxno == '' || $boxno == 'ALL') {

    $sql = "SELECT 
                pa.box_no,
                pa.product_code,
                pr.product_style,
                pr.product_name,
                SUM(pa.put_qty) AS qty
            FROM `put-away` pa
            JOIN products pr ON pa.product_code = pr.product_code
            WHERE pa.pick_no = '$pickno'
            GROUP BY pa.box_no, pa.product_code, pr.product_style, pr.product_name
            ORDER BY pa.box_no";

} else {

# =========================
# 🔥 SPECIFIC BOX ONLY
# =========================
    $sql = "SELECT 
                pa.box_no,
                pa.product_code,
                pr.product_style,
                pr.product_name,
                SUM(pa.put_qty) AS qty
            FROM `put-away` pa
            JOIN products pr ON pa.product_code = pr.product_code
            WHERE pa.pick_no = '$pickno'
            AND pa.box_no = '$boxno'
            GROUP BY pa.box_no, pa.product_code, pr.product_style, pr.product_name";
}

$result = mysqli_query($conn, $sql);

if ($result && mysqli_num_rows($result) > 0) {

    while ($row = mysqli_fetch_assoc($result)) {

        echo "Box# " . $row['box_no'] . "|" .
             $row['product_code'] . "|" .
             $row['product_style'] . "|" .
             $row['product_name'] . "|" .
             $row['qty'] . "\n";
    }

} else {
    echo "NO_DATA";
}
?>