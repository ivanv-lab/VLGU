<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $x = (float)($_POST['x'] ?? 0);
    $y = (float)($_POST['y'] ?? 0);
    $a = (float)($_POST['a'] ?? 0);
    $denominator = ($x - $a) * $y;
    if ($denominator == 0.0) {
        $result = 'Вычисление невозможно: знаменатель равен нулю.';
    } else {
        $n = ($x ** 2 - $y ** 2) / $denominator;
        $l = sin($n) ** 2;
        $result = sprintf('N = %.6f, L = %.6f', $n, $l);
    }
}
?>
<!doctype html><meta charset="UTF-8"><title>Задание 1</title>
<h1>Задание 1</h1><p>Вычисление N и L.</p>
<form method="post">x: <input name="x" type="number" step="any" required>
y: <input name="y" type="number" step="any" required>
a: <input name="a" type="number" step="any" required>
<button>Вычислить</button></form>
<?php if ($result !== null): ?><p><?= htmlspecialchars($result, ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
