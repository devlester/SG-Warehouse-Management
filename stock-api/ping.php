<?php
header("Content-Type: text/plain");
$conn = mysqli_connect("localhost","root","","stocktake");

if ($conn) {
    echo "CONNECTED";
} else {
    echo "DISCONNECTED";
}
?>
