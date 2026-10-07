<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $a = array_map('floatval', $_POST['a'] ?? []); $min = min($a); $max = max($a); $p = ($min + $max) ** 2; $new = [];
    for ($r = 0; $r < 5; $r++) for ($c = 0; $c < 3; $c++) $new[$r][$c] = $a[$r * 3 + $c] + ($r === 3 ? 0 : $p);
    $result = [$min, $max, $p, $new];
}
?><!doctype html><meta charset="UTF-8"><title>Задание 8</title><h1>Задание 8</h1>
<form method="post"><p>Матрица A (5×3):</p><?php for ($i = 0; $i < 15; $i++): ?><input name="a[]" type="number" step="any" required><?php if (($i + 1) % 3 === 0): ?><br><?php endif; ?><?php endfor; ?><button>Построить</button></form>
<?php if ($result): ?><p>Amin = <?= $result[0] ?>, Amax = <?= $result[1] ?>, P = (Amin + Amax)² = <?= $result[2] ?></p><p>Новая матрица:</p><?php foreach ($result[3] as $row): ?><div><?= htmlspecialchars(implode(' | ', $row), ENT_QUOTES, 'UTF-8') ?></div><?php endforeach; ?><?php endif; ?>
