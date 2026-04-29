<?php
header("Content-Type: application/json");
$conn = mysqli_connect("localhost", "root", "", "stocktake");

$product   = $_POST['product'];
$warehouse = $_POST['warehouse'];
$qty       = intval($_POST['qty']);
$batch     = $_POST['batch'];

mysqli_query($conn,
    "INSERT INTO batches(batch_no, product_name, warehouse_name, quantity)
     VALUES('$batch', '$product', '$warehouse', $qty)");

mysqli_query($conn,
    "INSERT INTO stock_balance(product_name, warehouse_name, quantity)
     VALUES('$product', '$warehouse', $qty)
     ON DUPLICATE KEY UPDATE quantity = quantity + $qty");

echo json_encode(["status" => "ok", "message" => "STOCK IN SAVED"]);
?>
