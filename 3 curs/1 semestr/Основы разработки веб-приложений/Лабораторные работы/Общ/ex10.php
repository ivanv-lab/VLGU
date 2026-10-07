<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $a = array_map('floatval', $_POST['a'] ?? []); $b = array_map('floatval', $_POST['b'] ?? []); $d = array_merge(array_chunk($a, 5), array_chunk($b, 5)); $average = array_sum(array_merge($a, $b)) / 25; $result = $average == 0.0 ? 'Среднее арифметическое равно нулю, деление невозможно.' : ['average' => $average, 'matrix' => array_map(fn($row) => array_map(fn($v) => $v / $average, $row), $d)]; }
function inputs(string $name, int $count): string { $html = ''; for ($i = 0; $i < $count; $i++) $html .= '<input name="' . $name . '[]" type="number" step="any" required>'; return $html; }
?><!doctype html><meta charset="UTF-8"><title>Задание 10</title><h1>Задание 10</h1>
<form method="post"><p>A (2×5):</p><?= inputs('a', 10) ?><p>B (3×5):</p><?= inputs('b', 15) ?><br><button>Объединить</button></form>
<?php if (is_array($result)): ?><p>Среднее арифметическое D: <?= $result['average'] ?></p><?php foreach ($result['matrix'] as $row): ?><div><?= htmlspecialchars(implode(' | ', $row), ENT_QUOTES, 'UTF-8') ?></div><?php endforeach; ?><?php elseif ($result): ?><p><?= htmlspecialchars($result, ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
