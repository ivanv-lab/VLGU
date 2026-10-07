<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $codes = $_POST['code'] ?? []; $dtp = array_map('intval', $_POST['dtp'] ?? []); $result = [[], [], []];
    foreach ($dtp as $i => $count) { if ($count === 0) $result[0][] = $codes[$i]; if ($count <= 10) $result[1][] = $codes[$i]; if ($count <= 80) $result[2][] = $codes[$i]; }
}
?><!doctype html><meta charset="UTF-8"><title>Задание 9</title><h1>Задание 9</h1>
<form method="post"><?php for ($i = 0; $i < 20; $i++): ?><input name="code[]" placeholder="GAI<?= $i + 1 ?>" required><input name="dtp[]" type="number" min="0" required><br><?php endfor; ?><button>Сформировать</button></form>
<?php if ($result): ?><?php foreach ($result as $i => $list): ?><p><?= $i + 1 ?>: <?= htmlspecialchars($list ? implode(', ', $list) : 'нет', ENT_QUOTES, 'UTF-8') ?></p><?php endforeach; ?><?php endif; ?>
