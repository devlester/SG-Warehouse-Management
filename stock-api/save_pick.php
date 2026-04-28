<?php
header("Content-Type: text/plain");

$conn = mysqli_connect("localhost", "root", "", "stocktake");

if (!$conn) {
    echo "ERROR|DB";
    exit;
}

// 👉 GET DATA
$barcode       = $_POST['barcode'] ?? $_GET['barcode'] ?? '';
$qty           = floatval($_POST['picked_qty'] ?? $_GET['picked_qty'] ?? 0);
$pickno        = $_POST['pick_no'] ?? $_GET['pick_no'] ?? '';
$boxno         = trim($_POST['box_no'] ?? $_GET['box_no'] ?? '');
$from_location = $_POST['from_location'] ?? $_GET['from_location'] ?? '';

// 👉 VALIDATE
if ($barcode == '' || $qty <= 0 || $pickno == '') {
    echo "ERROR|INVALID_INPUT";
    exit;
}

# =========================
# 🔥 GET REQUIRED QTY
# =========================
$sql_req = "SELECT qty_to_pick 
            FROM picking_items
            WHERE product_code = '$barcode'
            AND pick_no = '$pickno'
            AND product_location = '$from_location'
            LIMIT 1";

$res_req = mysqli_query($conn, $sql_req);

if (!$res_req || mysqli_num_rows($res_req) == 0) {
    echo "ERROR|NOT_FOUND";
    exit;
}

$row_req = mysqli_fetch_assoc($res_req);
$qty_to_pick = $row_req['qty_to_pick'];

# =========================
# 🔥 CURRENT PICKED
# =========================
$sql_total = "SELECT IFNULL(SUM(picked_qty),0) AS total
              FROM picked_items
              WHERE product_code = '$barcode'
              AND pick_no = '$pickno'";

$res_total = mysqli_query($conn, $sql_total);
$row_total = mysqli_fetch_assoc($res_total);

$current_picked = $row_total['total'];

# =========================
# 🔥 IF NO BOX → PICKING
# =========================
if ($boxno == '') {

    // 👉 OVER PICK CHECK
    if (($current_picked + $qty) > $qty_to_pick) {
        echo "ERROR|OVERPICK";
        exit;
    }

    // 👉 INSERT / UPDATE PICK
    $sql_check = "SELECT pick_id, picked_qty 
                  FROM picked_items
                  WHERE product_code = '$barcode'
                  AND pick_no = '$pickno'
                  LIMIT 1";

    $result_check = mysqli_query($conn, $sql_check);

    if ($result_check && mysqli_num_rows($result_check) > 0) {

        $row = mysqli_fetch_assoc($result_check);
        $new_qty = $row['picked_qty'] + $qty;

        $sql_update = "UPDATE picked_items 
                       SET picked_qty = '$new_qty',
                           picked_at = NOW()
                       WHERE pick_id = '".$row['pick_id']."'";

        echo mysqli_query($conn, $sql_update) ? "SUCCESS|PICK_UPDATED" : "ERROR|UPDATE_FAIL";

    } else {

        $sql_insert = "INSERT INTO picked_items (
                            pick_no,
                            product_code,
                            picked_qty,
                            picked_by,
                            picked_at
                        ) VALUES (
                            '$pickno',
                            '$barcode',
                            '$qty',
                            'SYSTEM',
                            NOW()
                        )";

        echo mysqli_query($conn, $sql_insert) ? "SUCCESS|PICK_INSERTED" : "ERROR|INSERT_FAIL";
    }

# =========================
# 🔥 IF WITH BOX → PUTAWAY ONLY
# =========================
} else {

    // 👉 GET TOTAL BOXED
    $sql_boxed = "SELECT IFNULL(SUM(put_qty),0) AS total
                  FROM `put-away`
                  WHERE product_code = '$barcode'
                  AND pick_no = '$pickno'";

    $res_boxed = mysqli_query($conn, $sql_boxed);
    $row_boxed = mysqli_fetch_assoc($res_boxed);
    $total_boxed = $row_boxed['total'];

    // 👉 COMPUTE REMAINING
    $remaining = $current_picked - $total_boxed;

    // 👉 OVER BOX CHECK
    if ($qty > $remaining) {
        echo "ERROR|OVERBOX";
        exit;
    }

    // 👉 INSERT PUTAWAY
    $sql_put = "INSERT INTO `put-away` (
                    pick_no,
                    product_code,
                    box_no,
                    put_qty,
                    put_at
                ) VALUES (
                    '$pickno',
                    '$barcode',
                    '$boxno',
                    '$qty',
                    NOW()
                )";

    echo mysqli_query($conn, $sql_put) ? "SUCCESS|PUTAWAY" : "ERROR|PUT_FAIL";
}
?>