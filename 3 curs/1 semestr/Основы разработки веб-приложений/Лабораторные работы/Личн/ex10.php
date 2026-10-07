<?php
function matrixInput(string $name, int $rows, int $cols): string { $html = ''; for ($i = 0; $i < $rows * $cols; $i++) { $html .= '<input name="' . $name . '[]" type="number" step="any" required>'; if (($i + 1) % $cols === 0) $html .= '<br>'; } return $html; }
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $a = array_map('floatval', $_POST['a'] ?? []); $b = array_map('floatval', $_POST['b'] ?? []); $c = array_map('floatval', $_POST['c'] ?? []);
    $d = []; for ($r = 0; $r < 4; $r++) { $d[$r] = []; for ($j = 0; $j < 4; $j++) $d[$r][$j] = $r < 3 ? $a[$r * 4 + $j] : 0; for ($j = 0; $j < 2; $j++) $d[$r][] = $b[$r * 2 + $j]; }
    $result = $d;
}
?><!doctype html><meta charset="UTF-8"><title>Задание 10</title><h1>Задание 10</h1><p>A (3×4), B (4×2), C (5×5). Для горизонтального объединения A дополнена нулевой строкой.</p>
<form method="post"><p>A:</p><?= matrixInput('a', 3, 4) ?><p>B:</p><?= matrixInput('b', 4, 2) ?><p>C:</p><?= matrixInput('c', 5, 5) ?><button>Объединить и проверить</button></form>
<?php if ($result): ?><p>D имеет размер 4×6.</p><?php foreach ($result as $row): ?><div><?= htmlspecialchars(implode(' | ', $row), ENT_QUOTES, 'UTF-8') ?></div><?php endforeach; ?><p>D×C и C×D не определены: внутренние размеры не совпадают.</p><?php endif; ?>
