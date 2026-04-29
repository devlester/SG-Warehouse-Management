<?php
header("Content-Type: application/json");
$conn = mysqli_connect("localhost", "root", "", "stocktake");
echo json_encode(["status" => $conn ? "connected" : "disconnected"]);
?>
