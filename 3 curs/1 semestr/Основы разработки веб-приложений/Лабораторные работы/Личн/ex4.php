<?php
$result = [];
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $a = array_map('floatval', $_POST['a'] ?? []); $sumSquares = array_sum(array_map(fn($v) => $v ** 2, $a));
    foreach ($a as $i => $value) $result[] = $sumSquares == 0.0 ? null : sqrt(abs($value)) / (6 / $sumSquares);
}
?><!doctype html><meta charset="UTF-8"><title>Задание 4</title><h1>Задание 4</h1>
<form method="post"><?php for ($i = 0; $i < 6; $i++): ?><input name="a[]" type="number" step="any" required><?php endfor; ?><button>Вычислить</button></form>
<?php if ($result): ?><p>K: <?= htmlspecialchars(implode('; ', array_map(fn($v) => $v === null ? 'не определено' : number_format($v, 6, '.', ''), $result)), ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
