<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $x = (float)($_POST['x'] ?? 0); $result = (1 - $x ** 5 / 5 + $x ** 10 / 10 - $x ** 15 / 15 + $x ** 20 / 20) / 2; }
?><!doctype html><meta charset="UTF-8"><title>Задание 5</title><h1>Задание 5</h1>
<form method="post">x: <input name="x" type="number" step="any" required><button>Вычислить</button></form>
<?php if ($result !== null): ?><p>y = <?= number_format($result, 6, '.', '') ?></p><?php endif; ?>
