<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $a = (float)($_POST['a'] ?? 0); $b = (float)($_POST['b'] ?? 0); $x = ($a + $b) * $a;
    $y = $a == $b ? ($a * $b == 0.0 ? null : $x / ($a * $b)) : $x ** 2 * ($a - $b);
    if ($y === null) $result = 'y не определяется: a·b равно нулю.';
    else if ($x == 0.0 && $y > 2) $result = "x = $x, y = $y; z не определяется: x·y равно нулю.";
    else { $z = $y <= 2 ? ($y == 0.0 ? null : $x / $y) : ($a * $b) / ($x * $y); $result = $z === null ? "x = $x, y = $y; z не определяется." : sprintf('x = %.6f, y = %.6f, z = %.6f', $x, $y, $z); }
}
?><!doctype html><meta charset="UTF-8"><title>Задание 2</title><h1>Задание 2</h1>
<form method="post">a: <input name="a" type="number" step="any" required> b: <input name="b" type="number" step="any" required> <button>Вычислить</button></form>
<?php if ($result !== null): ?><p><?= htmlspecialchars($result, ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
