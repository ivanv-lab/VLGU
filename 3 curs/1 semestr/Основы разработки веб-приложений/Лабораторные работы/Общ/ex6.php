<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $values = array_values(array_map('floatval', $_POST['y'] ?? [])); $min = min($values); $max = max($values); foreach ($values as &$value) { if ($value == $min) $value = $max; else if ($value == $max) $value = $min; } unset($value); $result = $values; }
?><!doctype html><meta charset="UTF-8"><title>Задание 6</title><h1>Задание 6</h1>
<form method="post"><?php for ($i = 0; $i < 15; $i++): ?><input name="y[]" type="number" step="any" required><?php endfor; ?><br><button>Поменять местами</button></form>
<?php if ($result !== null): ?><p>Новый массив: <?= htmlspecialchars(implode(', ', $result), ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
