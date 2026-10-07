<?php
$result = null;
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $result = []; foreach ($_POST['faculty'] ?? [] as $faculty) if (preg_match('/^дсф$/ui', trim($faculty))) $result[] = $faculty; }
?><!doctype html><meta charset="UTF-8"><title>Задание 3</title><h1>Задание 3</h1>
<form method="post"><?php for ($i = 1; $i <= 4; $i++): ?>Факультет <?= $i ?>: <input name="faculty[]" required><br><?php endfor; ?><button>Проверить</button></form>
<?php if ($result !== null): ?><p><?= $result ? 'Факультет ДСФ найден.' : 'Факультет ДСФ отсутствует.' ?></p><?php endif; ?>
