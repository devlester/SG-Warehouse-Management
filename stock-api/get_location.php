<?php
header("Content-Type: text/plain");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo "ERROR|DB";
    exit;
}
    
$sql = "SELECT store_location,pick_no FROM picking_orders WHERE status='OPEN'";
$result = mysqli_query($conn, $sql);

if (!$result) {
    echo "ERROR|QUERY";
    exit;
}

while ($row = mysqli_fetch_assoc($result)) {
    echo $row['store_location'] . "|" . $row['pick_no'] . "\n";
}

mysqli_close($conn);
?>