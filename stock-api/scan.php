<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$year_collection = $_POST['year_collection'] ?? '';
$product_code    = $_POST['product_code']    ?? '';

if ($year_collection == '' || $product_code == '') {
    echo json_encode(["status" => "error", "message" => "PARAM"]);
    exit;
}

$sql  = "SELECT p.product_style, p.product_name
         FROM incoming_products ip
         INNER JOIN products p ON ip.product_code = p.product_code
         WHERE ip.year_collection = ?
         AND   ip.product_code    = ?
         LIMIT 1";

$stmt = mysqli_prepare($conn, $sql);
mysqli_stmt_bind_param($stmt, "ss", $year_collection, $product_code);
mysqli_stmt_execute($stmt);
mysqli_stmt_store_result($stmt);

if (mysqli_stmt_num_rows($stmt) > 0) {
    mysqli_stmt_bind_result($stmt, $product_style, $product_name);
    mysqli_stmt_fetch($stmt);
    echo json_encode(["status" => "found", "style" => $product_style, "name" => $product_name]);
} else {
    echo json_encode(["status" => "not_found"]);
}

mysqli_close($conn);
?>
