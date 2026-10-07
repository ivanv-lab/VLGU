<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $names = $_POST['bank'] ?? []; $rates = array_map('floatval', $_POST['rate'] ?? []); $average = array_sum($rates) / max(count($rates), 1); $below = [];
    foreach ($rates as $i => $rate) if ($rate < $average) $below[] = ($names[$i] ?? '') . ' (' . $rate . '%)';
    $maxIndex = array_search(max($rates), $rates, true); $result = [$average, $below, $names[$maxIndex] ?? ''];
}
?><!doctype html><meta charset="UTF-8"><title>Задание 7</title><h1>Задание 7</h1>
<form method="post"><?php for ($i = 0; $i < 10; $i++): ?><input name="bank[]" placeholder="Банк <?= $i + 1 ?>" required><input name="rate[]" type="number" step="any" placeholder="%" required><br><?php endfor; ?><button>Обработать</button></form>
<?php if ($result): ?><p>Средняя ставка: <?= number_format($result[0], 2, '.', '') ?>%</p><p>Ниже средней: <?= htmlspecialchars($result[1] ? implode(', ', $result[1]) : 'нет', ENT_QUOTES, 'UTF-8') ?></p><p>Максимальная ставка: <?= htmlspecialchars($result[2], ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
