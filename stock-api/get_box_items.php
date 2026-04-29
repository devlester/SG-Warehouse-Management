<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$pickno = $_GET['pick_no'] ?? '';
$boxno  = $_GET['box_no']  ?? '';

if ($pickno == '') {
    echo json_encode(["status" => "error", "message" => "INVALID_INPUT"]);
    exit;
}

if ($boxno == '' || $boxno == 'ALL') {
    $sql = "SELECT pa.box_no, pa.product_code,
                   pr.product_style, pr.product_name,
                   SUM(pa.put_qty) AS qty
            FROM `put-away` pa
            JOIN products pr ON pa.product_code = pr.product_code
            WHERE pa.pick_no = '$pickno'
            GROUP BY pa.box_no, pa.product_code, pr.product_style, pr.product_name
            ORDER BY pa.box_no";
} else {
    $sql = "SELECT pa.box_no, pa.product_code,
                   pr.product_style, pr.product_name,
                   SUM(pa.put_qty) AS qty
            FROM `put-away` pa
            JOIN products pr ON pa.product_code = pr.product_code
            WHERE pa.pick_no = '$pickno'
            AND   pa.box_no  = '$boxno'
            GROUP BY pa.box_no, pa.product_code, pr.product_style, pr.product_name";
}

$result = mysqli_query($conn, $sql);
$items  = [];

if ($result && mysqli_num_rows($result) > 0) {
    while ($row = mysqli_fetch_assoc($result)) {
        $items[] = [
            "box_no"       => $row['box_no'],
            "product_code" => $row['product_code'],
            "style"        => $row['product_style'],
            "name"         => $row['product_name'],
            "qty"          => (int)$row['qty']
        ];
    }
}

echo json_encode(["items" => $items]);
?>
