<?php
header("Content-Type: text/plain");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo "ERROR|DB";
    exit;
}

$sql = "SELECT TRIM(warehouse_name) AS warehouse_name 
        FROM warehouses 
        ORDER BY warehouse_name";

$result = mysqli_query($conn, $sql);

if (!$result) {
    echo "ERROR|QUERY";
    exit;
}

while ($row = mysqli_fetch_assoc($result)) {
    if ($row['warehouse_name'] != "") {
        echo $row['warehouse_name'] . "\n";
    }
}


mysqli_close($conn);
?>
