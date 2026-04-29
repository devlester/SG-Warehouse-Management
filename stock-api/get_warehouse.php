<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$sql    = "SELECT TRIM(warehouse_name) AS warehouse_name FROM warehouses ORDER BY warehouse_name";
$result = mysqli_query($conn, $sql);

if (!$result) {
    echo json_encode(["status" => "error", "message" => "QUERY"]);
    exit;
}

$warehouses = [];
while ($row = mysqli_fetch_assoc($result)) {
    if ($row['warehouse_name'] != "") {
        $warehouses[] = $row['warehouse_name'];
    }
}

echo json_encode(["warehouses" => $warehouses]);
mysqli_close($conn);
?>
