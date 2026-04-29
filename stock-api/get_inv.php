<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$sql    = "SELECT year_collection FROM `incoming_products` GROUP BY year_collection";
$result = mysqli_query($conn, $sql);

if (!$result) {
    echo json_encode(["status" => "error", "message" => "QUERY"]);
    exit;
}

$collections = [];
while ($row = mysqli_fetch_assoc($result)) {
    if ($row['year_collection'] != "") {
        $collections[] = $row['year_collection'];
    }
}

echo json_encode(["collections" => $collections]);
mysqli_close($conn);
?>
