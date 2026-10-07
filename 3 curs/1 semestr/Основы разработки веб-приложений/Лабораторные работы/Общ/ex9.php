<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $points = $_POST['point'] ?? []; $distances = array_map('floatval', $_POST['distance'] ?? []); $pairs = [['Тула', 'Орел'], ['Курск', 'Белгород'], ['Харьков', 'Запорожье']]; $result = []; foreach ($pairs as [$from, $to]) { $i = array_search($from, $points, true); $j = array_search($to, $points, true); $result[] = ($i === false || $j === false) ? "$from — $to: пункты не найдены" : "$from — $to: " . abs($distances[$j] - $distances[$i]); } }
?><!doctype html><meta charset="UTF-8"><title>Задание 9</title><h1>Задание 9</h1>
<form method="post"><?php for ($i = 0; $i < 20; $i++): ?><input name="point[]" placeholder="S<?= $i + 1 ?>" required><input name="distance[]" type="number" step="any" placeholder="R<?= $i + 1 ?>" required><br><?php endfor; ?><button>Найти расстояния</button></form>
<?php if ($result): ?><?php foreach ($result as $line): ?><p><?= htmlspecialchars($line, ENT_QUOTES, 'UTF-8') ?></p><?php endforeach; ?><?php endif; ?>
