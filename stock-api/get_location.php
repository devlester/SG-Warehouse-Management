<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$sql    = "SELECT store_location, pick_no FROM picking_orders WHERE status='OPEN'";
$result = mysqli_query($conn, $sql);

if (!$result) {
    echo json_encode(["status" => "error", "message" => "QUERY"]);
    exit;
}

$locations = [];
while ($row = mysqli_fetch_assoc($result)) {
    $locations[] = ["store" => $row['store_location'], "pick_no" => $row['pick_no']];
}

echo json_encode(["locations" => $locations]);
mysqli_close($conn);
?>
