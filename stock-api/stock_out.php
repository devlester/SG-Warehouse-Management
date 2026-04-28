<?php
header("Content-Type: text/plain");
$conn = mysqli_connect("localhost","root","","stocktake");


$product = $_POST['product'];
$warehouse = $_POST['warehouse'];
$qty = intval($_POST['qty']);


mysqli_query($conn,"UPDATE stock_balance SET quantity = quantity - $qty WHERE product_name='$product' AND warehouse_name='$warehouse'");


echo "OK|STOCK OUT SAVED";
?>