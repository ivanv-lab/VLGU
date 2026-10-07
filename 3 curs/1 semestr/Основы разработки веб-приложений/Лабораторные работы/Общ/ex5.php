<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $x = (float)($_POST['x'] ?? 0); $result = $x + 2 * $x ** 2 / 3 + 4 * $x ** 3 / 9 + 8 * $x ** 4 / 27 + 16 * $x ** 5 / 81 + 32 * $x ** 6 / 243 + 64 * $x ** 7 / 729; }
?><!doctype html><meta charset="UTF-8"><title>Задание 5</title><h1>Задание 5</h1>
<form method="post">x: <input name="x" type="number" step="any" required><button>Вычислить</button></form>
<?php if ($result !== null): ?><p>y = <?= number_format($result, 6, '.', '') ?></p><?php endif; ?>
