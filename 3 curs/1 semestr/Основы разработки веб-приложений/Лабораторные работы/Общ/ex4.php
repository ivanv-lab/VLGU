<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $x = array_map('floatval', $_POST['x'] ?? []); $y = array_map('floatval', $_POST['y'] ?? []); $result = []; foreach ($x as $i => $value) $result[] = sqrt($value ** 2 + ($y[$i] ?? 0) ** 2); }
?><!doctype html><meta charset="UTF-8"><title>Задание 4</title><h1>Задание 4</h1>
<form method="post"><p>Введите пары xᵢ и yᵢ:</p><?php for ($i = 0; $i < 8; $i++): ?><input name="x[]" type="number" step="any" placeholder="x<?= $i + 1 ?>" required><input name="y[]" type="number" step="any" placeholder="y<?= $i + 1 ?>" required><br><?php endfor; ?><button>Вычислить</button></form>
<?php if ($result !== null): ?><p>Iᵢ: <?= htmlspecialchars(implode(', ', array_map(fn($v) => number_format($v, 6, '.', ''), $result)), ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
