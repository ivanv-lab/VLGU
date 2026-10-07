<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $rows = max(1, (int)($_POST['rows'] ?? 3)); $cols = max(1, (int)($_POST['cols'] ?? 4)); $skip = max(1, min($rows, (int)($_POST['skip'] ?? 1))) - 1; $values = array_map('floatval', $_POST['matrix'] ?? []); $means = []; for ($r = 0; $r < $rows; $r++) $means[$r] = array_sum(array_slice($values, $r * $cols, $cols)) / $cols; $maxMean = max($means); $new = []; for ($r = 0; $r < $rows; $r++) for ($c = 0; $c < $cols; $c++) $new[$r][$c] = $values[$r * $cols + $c] - ($r === $skip ? 0 : $maxMean); $result = [$means, $maxMean, $new]; }
?><!doctype html><meta charset="UTF-8"><title>Задание 8</title><h1>Задание 8</h1>
<form method="post">Строк: <input id="rows" name="rows" type="number" min="1" value="3" required> Столбцов: <input id="cols" name="cols" type="number" min="1" value="4" required> Не изменять строку №: <input name="skip" type="number" min="1" value="1" required><p>Матрица:</p><div id="matrixInputs"></div><button>Обработать</button></form>
<?php if ($result): ?><p>Средние строк: <?= htmlspecialchars(implode(', ', $result[0]), ENT_QUOTES, 'UTF-8') ?>; максимальное среднее: <?= $result[1] ?></p><?php foreach ($result[2] as $row): ?><div><?= htmlspecialchars(implode(' | ', $row), ENT_QUOTES, 'UTF-8') ?></div><?php endforeach; ?><?php endif; ?>
<script>
const rows = document.getElementById('rows'), cols = document.getElementById('cols'), matrixInputs = document.getElementById('matrixInputs');
function buildMatrixInputs() { matrixInputs.innerHTML = ''; for (let i = 0; i < Number(rows.value) * Number(cols.value); i++) { const input = document.createElement('input'); input.name = 'matrix[]'; input.type = 'number'; input.step = 'any'; input.required = true; matrixInputs.append(input); if ((i + 1) % Number(cols.value) === 0) matrixInputs.append(document.createElement('br')); } }
rows.addEventListener('input', buildMatrixInputs); cols.addEventListener('input', buildMatrixInputs); buildMatrixInputs();
</script>
