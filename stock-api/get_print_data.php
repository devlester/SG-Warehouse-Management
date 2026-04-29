<?php
$pickno = $_GET['pick_no']        ?? '';
$store  = $_GET['store_location'] ?? '';
$format = $_GET['format']         ?? 'json';

if ($pickno == '') {
    if ($format === 'html') { echo "<p>Error: pick_no is required.</p>"; }
    else { header("Content-Type: application/json"); echo json_encode(["status"=>"error","message"=>"INVALID_INPUT"]); }
    exit;
}

$conn = mysqli_connect("localhost", "root", "", "stocktake");
if (!$conn) {
    if ($format === 'html') { echo "<p>Database connection failed.</p>"; }
    else { header("Content-Type: application/json"); echo json_encode(["status"=>"error","message"=>"DB"]); }
    exit;
}

// ── totals ────────────────────────────────────────────────────
$totals = $conn->query("
    SELECT
        (SELECT IFNULL(SUM(qty_to_pick), 0) FROM picking_items WHERE pick_no = '$pickno') AS total_to_pick,
        (SELECT IFNULL(SUM(picked_qty),  0) FROM picked_items  WHERE pick_no = '$pickno') AS total_picked,
        (SELECT IFNULL(SUM(put_qty),     0) FROM `put-away`    WHERE pick_no = '$pickno') AS total_boxed
")->fetch_assoc();

// ── line items ────────────────────────────────────────────────
$itemsResult = $conn->query("
    SELECT
        pi.product_location,
        pr.product_style,
        pr.product_name,
        pi.qty_to_pick,
        IFNULL(pk.total_picked, 0)                    AS qty_picked,
        IFNULL(pa.total_boxed,  0)                    AS qty_boxed,
        (pi.qty_to_pick - IFNULL(pk.total_picked, 0)) AS discrepancy
    FROM picking_items pi
    JOIN products pr ON pi.product_code = pr.product_code
    LEFT JOIN (
        SELECT pick_no, product_code, SUM(picked_qty) AS total_picked
        FROM picked_items GROUP BY pick_no, product_code
    ) pk ON pi.pick_no = pk.pick_no AND pi.product_code = pk.product_code
    LEFT JOIN (
        SELECT pick_no, product_code, SUM(put_qty) AS total_boxed
        FROM `put-away` GROUP BY pick_no, product_code
    ) pa ON pi.pick_no = pa.pick_no AND pi.product_code = pa.product_code
    WHERE pi.pick_no = '$pickno'
    ORDER BY pi.product_location ASC
");

$items = [];
while ($row = $itemsResult->fetch_assoc()) { $items[] = $row; }

// ── box breakdown ─────────────────────────────────────────────
$boxResult = $conn->query("
    SELECT pa.box_no, pr.product_style, pr.product_name, SUM(pa.put_qty) AS qty
    FROM `put-away` pa
    JOIN products pr ON pa.product_code = pr.product_code
    WHERE pa.pick_no = '$pickno'
    GROUP BY pa.box_no, pa.product_code
    ORDER BY CAST(pa.box_no AS UNSIGNED), pa.product_code
");

$boxes = [];
while ($row = $boxResult->fetch_assoc()) { $boxes[] = $row; }

$printedAt = date("d/m/Y H:i");
$missing   = (int)$totals['total_to_pick'] - (int)$totals['total_picked'];

// ════════════════════════════════════════════════════════════
// HTML FORMAT
// ════════════════════════════════════════════════════════════
if ($format === 'html') {
    header("Content-Type: text/html; charset=utf-8");
?><!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>Pick List - <?= htmlspecialchars($pickno) ?></title>
<style>
  * { margin:0; padding:0; box-sizing:border-box; }
  body { font-family: Arial, sans-serif; font-size: 11px; color: #111; }

  .banner { background:#154783; color:#fff; padding:10px 14px; display:flex; justify-content:space-between; align-items:center; }
  .banner h1 { font-size:18px; letter-spacing:1px; }
  .banner span { font-size:12px; }

  .meta { display:flex; justify-content:space-between; padding:6px 14px 4px; border-bottom:1px solid #ccc; }
  .meta .store { font-weight:bold; font-size:13px; }
  .meta .date  { font-size:10px; color:#555; }

  .summary { display:flex; background:#e8eef7; padding:7px 14px; gap:30px; border-bottom:2px solid #154783; }
  .summary .item { display:flex; flex-direction:column; }
  .summary .label { font-size:9px; color:#555; text-transform:uppercase; letter-spacing:.5px; }
  .summary .value { font-size:15px; font-weight:bold; color:#154783; }
  .summary .warn  { font-size:15px; font-weight:bold; color:#b00; }

  .section-title { padding:10px 14px 4px; font-size:12px; font-weight:bold; text-transform:uppercase;
                   letter-spacing:.5px; color:#333; border-top:3px solid #154783; margin-top:14px; }

  table { width:100%; border-collapse:collapse; margin-bottom:6px; }
  thead tr { background:#444; color:#fff; }
  thead th { padding:5px 6px; text-align:left; font-size:10px; font-weight:bold; white-space:nowrap; }
  tbody tr:nth-child(even) { background:#f0f4ff; }
  tbody tr.disc { background:#ffd8d8; }
  tbody td { padding:4px 6px; border-bottom:1px solid #ddd; }
  .disc-val { color:#900; font-weight:bold; }
  .ok-val   { color:#090; }

  .box-head { background:#1e6e37 !important; color:#fff !important; }

  .footer { margin-top:18px; padding:6px 14px; border-top:1px solid #999;
            display:flex; justify-content:space-between; font-size:9px; color:#666; }

  @media print {
    .no-print { display:none; }
    body { font-size:10px; }
    .banner h1 { font-size:15px; }
    .section-title { margin-top:10px; padding-top:6px; }
  }
</style>
</head>
<body>

<div class="no-print" style="background:#fffbe6;padding:8px 14px;border-bottom:1px solid #e0c060;font-size:11px;">
  Press <strong>Ctrl+P</strong> (or File → Print) to print.
  To save as PDF, choose <strong>"Save as PDF"</strong> or <strong>"Microsoft Print to PDF"</strong> as the printer.
</div>

<div class="banner">
  <h1>PICK LIST REPORT</h1>
  <span>Pick #: <?= htmlspecialchars($pickno) ?> &nbsp;|&nbsp; <?= $printedAt ?></span>
</div>

<div class="meta">
  <span class="store">Store: <?= htmlspecialchars($store) ?></span>
  <span class="date">Printed: <?= $printedAt ?></span>
</div>

<div class="summary">
  <div class="item">
    <span class="label">Total to Pick</span>
    <span class="value"><?= (int)$totals['total_to_pick'] ?></span>
  </div>
  <div class="item">
    <span class="label">Total Picked</span>
    <span class="value"><?= (int)$totals['total_picked'] ?></span>
  </div>
  <div class="item">
    <span class="label">Total Boxed</span>
    <span class="value"><?= (int)$totals['total_boxed'] ?></span>
  </div>
  <?php if ($missing > 0): ?>
  <div class="item">
    <span class="label">Outstanding</span>
    <span class="warn">&#9888; <?= $missing ?> unit(s) not picked</span>
  </div>
  <?php endif; ?>
</div>

<!-- PICK ITEMS -->
<div class="section-title">Pick Items</div>
<table>
  <thead>
    <tr>
      <th>Location</th>
      <th>Style</th>
      <th>Product Name</th>
      <th style="text-align:right">To Pick</th>
      <th style="text-align:right">Picked</th>
      <th style="text-align:right">Boxed</th>
      <th style="text-align:right">Discrepancy</th>
    </tr>
  </thead>
  <tbody>
    <?php foreach ($items as $row):
      $disc = (int)$row['discrepancy'];
    ?>
    <tr class="<?= $disc > 0 ? 'disc' : '' ?>">
      <td><?= htmlspecialchars($row['product_location']) ?></td>
      <td><?= htmlspecialchars($row['product_style']) ?></td>
      <td><?= htmlspecialchars($row['product_name']) ?></td>
      <td style="text-align:right"><?= (int)$row['qty_to_pick'] ?></td>
      <td style="text-align:right"><?= (int)$row['qty_picked'] ?></td>
      <td style="text-align:right"><?= (int)$row['qty_boxed'] ?></td>
      <td style="text-align:right" class="<?= $disc > 0 ? 'disc-val' : 'ok-val' ?>">
        <?= $disc > 0 ? '&#9660; '.$disc : '&#10003;' ?>
      </td>
    </tr>
    <?php endforeach; ?>
  </tbody>
</table>

<!-- BOX BREAKDOWN -->
<div class="section-title" style="border-top-color:#1e6e37">Box Breakdown</div>
<table>
  <thead>
    <tr class="box-head">
      <th>Box #</th>
      <th>Style</th>
      <th>Product Name</th>
      <th style="text-align:right">Qty</th>
    </tr>
  </thead>
  <tbody>
    <?php foreach ($boxes as $row): ?>
    <tr>
      <td><strong>Box #<?= htmlspecialchars($row['box_no']) ?></strong></td>
      <td><?= htmlspecialchars($row['product_style']) ?></td>
      <td><?= htmlspecialchars($row['product_name']) ?></td>
      <td style="text-align:right"><?= (int)$row['qty'] ?></td>
    </tr>
    <?php endforeach; ?>
    <?php if (empty($boxes)): ?>
    <tr><td colspan="4" style="color:#888;text-align:center;padding:8px">No items boxed yet.</td></tr>
    <?php endif; ?>
  </tbody>
</table>

<div class="footer">
  <span>SG Warehouse Management System</span>
  <span>Pick #: <?= htmlspecialchars($pickno) ?> &nbsp;|&nbsp; <?= $printedAt ?></span>
</div>

</body>
</html>
<?php
    exit;
}

// ════════════════════════════════════════════════════════════
// JSON FORMAT (default — used by PickMainForm list view)
// ════════════════════════════════════════════════════════════
header("Content-Type: application/json");

$itemsJson = [];
foreach ($items as $row) {
    $itemsJson[] = [
        "location"    => $row['product_location'],
        "style"       => $row['product_style'],
        "name"        => $row['product_name'],
        "qty_to_pick" => (int)$row['qty_to_pick'],
        "qty_picked"  => (int)$row['qty_picked'],
        "qty_boxed"   => (int)$row['qty_boxed'],
        "discrepancy" => (int)$row['discrepancy']
    ];
}

$boxesJson = [];
foreach ($boxes as $row) {
    $boxesJson[] = [
        "box_no" => $row['box_no'],
        "style"  => $row['product_style'],
        "name"   => $row['product_name'],
        "qty"    => (int)$row['qty']
    ];
}

echo json_encode([
    "status"        => "ok",
    "store"         => $store,
    "pick_no"       => $pickno,
    "printed_at"    => $printedAt,
    "total_to_pick" => (int)$totals['total_to_pick'],
    "total_picked"  => (int)$totals['total_picked'],
    "total_boxed"   => (int)$totals['total_boxed'],
    "items"         => $itemsJson,
    "boxes"         => $boxesJson
]);
?>
