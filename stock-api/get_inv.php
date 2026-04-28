

<?php
header("Content-Type: text/plain");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo "ERROR|DB";
    exit;
}

$sql = "SELECT year_collection FROM `incoming_products` GROUP by year_collection";

$result = mysqli_query($conn, $sql);

if (!$result) {
    echo "ERROR|QUERY";
    exit;
}

while ($row = mysqli_fetch_assoc($result)) {
    if ($row['year_collection'] != "") {
        echo $row['year_collection'] . "\n";
    }
}


mysqli_close($conn);
?>
