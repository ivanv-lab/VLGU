<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $alpha = (float)($_POST['alpha'] ?? 0); $beta = (float)($_POST['beta'] ?? 0);
    $denominatorK = $beta ** 2 - $alpha;
    if ($denominatorK == 0.0) $result = 'K не определяется: знаменатель равен нулю.';
    else {
        $k = (($beta - $alpha) ** 2 / $denominatorK) * sin($alpha - $beta);
        $denominatorM = $k ** 3 - $beta;
        $result = $denominatorM == 0.0 ? "K = $k; m не определяется: знаменатель равен нулю." : sprintf('K = %.6f, m = %.6f', $k, ($k + sqrt(abs($alpha * $beta))) / $denominatorM);
    }
}
?><!doctype html><meta charset="UTF-8"><title>Задание 1</title><h1>Задание 1</h1>
<form method="post">α: <input name="alpha" type="number" step="any" required> β: <input name="beta" type="number" step="any" required> <button>Вычислить</button></form>
<?php if ($result !== null): ?><p><?= htmlspecialchars($result, ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
