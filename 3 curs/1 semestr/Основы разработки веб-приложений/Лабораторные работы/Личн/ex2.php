<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $l = (float)($_POST['l'] ?? 0); $n = (int)($_POST['n'] ?? 0); $q = (float)($_POST['q'] ?? 0);
    $k1 = $l ** 2 + $n ** 2;
    $k2 = $l >= $n ? 1 / max($n, 1) : (1 - $n) * $q;
    $result = sprintf('K1 = %.6f, K2 = %.6f', $k1, $k2);
}
?><!doctype html><meta charset="UTF-8"><title>Задание 2</title>
<h1>Задание 2</h1><form method="post">l: <input name="l" type="number" step="any" required>
n: <input name="n" type="number" required> q: <input name="q" type="number" step="any" required>
<button>Вычислить</button></form><?php if ($result): ?><p><?= htmlspecialchars($result, ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
