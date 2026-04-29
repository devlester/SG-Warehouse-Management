<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");

$user = $_POST['username'] ?? '';
$pass = $_POST['password'] ?? '';

$sql  = "SELECT fullname, warehouse_id FROM users WHERE username=? AND password=?";
$stmt = mysqli_prepare($conn, $sql);
mysqli_stmt_bind_param($stmt, "ss", $user, $pass);
mysqli_stmt_execute($stmt);
mysqli_stmt_store_result($stmt);

if (mysqli_stmt_num_rows($stmt) > 0) {
    mysqli_stmt_bind_result($stmt, $name, $wh);
    mysqli_stmt_fetch($stmt);
    echo json_encode(["status" => "ok", "fullname" => $name, "warehouse_id" => $wh]);
} else {
    echo json_encode(["status" => "invalid"]);
}
?>
