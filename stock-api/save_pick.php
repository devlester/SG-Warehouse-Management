<?php
header("Content-Type: application/json");

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    echo json_encode(["status" => "error", "message" => "DB"]);
    exit;
}

$barcode       = $_POST['barcode']       ?? $_GET['barcode']       ?? '';
$qty           = floatval($_POST['picked_qty']  ?? $_GET['picked_qty']  ?? 0);
$pickno        = $_POST['pick_no']       ?? $_GET['pick_no']       ?? '';
$boxno         = trim($_POST['box_no']   ?? $_GET['box_no']        ?? '');
$from_location = $_POST['from_location'] ?? $_GET['from_location'] ?? '';

if ($barcode == '' || $qty <= 0 || $pickno == '') {
    echo json_encode(["status" => "error", "message" => "INVALID_INPUT"]);
    exit;
}

// ── required qty ─────────────────────────────────────────────
$res_req = mysqli_query($conn,
    "SELECT qty_to_pick FROM picking_items
     WHERE product_code     = '$barcode'
     AND   pick_no          = '$pickno'
     AND   product_location = '$from_location'
     LIMIT 1");

if (!$res_req || mysqli_num_rows($res_req) == 0) {
    echo json_encode(["status" => "error", "message" => "NOT_FOUND"]);
    exit;
}

$qty_to_pick = mysqli_fetch_assoc($res_req)['qty_to_pick'];

// ── current picked total ──────────────────────────────────────
$res_total      = mysqli_query($conn,
    "SELECT IFNULL(SUM(picked_qty), 0) AS total
     FROM picked_items
     WHERE product_code = '$barcode'
     AND   pick_no      = '$pickno'");
$current_picked = mysqli_fetch_assoc($res_total)['total'];

// ── picking (no box) ─────────────────────────────────────────
if ($boxno == '') {

    if (($current_picked + $qty) > $qty_to_pick) {
        echo json_encode(["status" => "error", "message" => "OVERPICK"]);
        exit;
    }

    $result_check = mysqli_query($conn,
        "SELECT pick_id, picked_qty FROM picked_items
         WHERE product_code = '$barcode'
         AND   pick_no      = '$pickno'
         LIMIT 1");

    if ($result_check && mysqli_num_rows($result_check) > 0) {
        $row     = mysqli_fetch_assoc($result_check);
        $new_qty = $row['picked_qty'] + $qty;
        $ok      = mysqli_query($conn,
            "UPDATE picked_items
             SET picked_qty = '$new_qty', picked_at = NOW()
             WHERE pick_id  = '" . $row['pick_id'] . "'");
        echo json_encode($ok
            ? ["status" => "success", "action" => "PICK_UPDATED"]
            : ["status" => "error",   "message" => "UPDATE_FAIL"]);
    } else {
        $ok = mysqli_query($conn,
            "INSERT INTO picked_items (pick_no, product_code, picked_qty, picked_by, picked_at)
             VALUES ('$pickno', '$barcode', '$qty', 'SYSTEM', NOW())");
        echo json_encode($ok
            ? ["status" => "success", "action" => "PICK_INSERTED"]
            : ["status" => "error",   "message" => "INSERT_FAIL"]);
    }

// ── putaway (with box) ───────────────────────────────────────
} else {

    $res_boxed   = mysqli_query($conn,
        "SELECT IFNULL(SUM(put_qty), 0) AS total
         FROM `put-away`
         WHERE product_code = '$barcode'
         AND   pick_no      = '$pickno'");
    $total_boxed = mysqli_fetch_assoc($res_boxed)['total'];
    $remaining   = $current_picked - $total_boxed;

    if ($qty > $remaining) {
        echo json_encode(["status" => "error", "message" => "OVERBOX"]);
        exit;
    }

    $ok = mysqli_query($conn,
        "INSERT INTO `put-away` (pick_no, product_code, box_no, put_qty, put_at)
         VALUES ('$pickno', '$barcode', '$boxno', '$qty', NOW())");
    echo json_encode($ok
        ? ["status" => "success", "action" => "PUTAWAY"]
        : ["status" => "error",   "message" => "PUT_FAIL"]);
}
?>
