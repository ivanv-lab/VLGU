<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $values = array_values(array_map('floatval', $_POST['y'] ?? [])); $min = min($values); $minIndex = array_search($min, $values, true);
    if ($minIndex < 5) foreach ($values as &$value) if ($value < 0) $value = $min; unset($value);
    $result = ['min' => $min, 'index' => $minIndex + 1, 'values' => $values];
}
?><!doctype html><meta charset="UTF-8"><title>Задание 6</title><h1>Задание 6</h1>
<form method="post"><?php for ($i = 1; $i <= 10; $i++): ?><input name="y[]" type="number" step="any" required><?php endfor; ?><button>Обработать</button></form>
<?php if ($result): ?><p>Минимум: <?= $result['min'] ?>, индекс: <?= $result['index'] ?></p><p>Массив: <?= htmlspecialchars(implode(', ', $result['values']), ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
