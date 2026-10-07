<?php
$words = [];
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    for ($i = 1; $i <= 3; $i++) {
        $word = trim((string)($_POST["word$i"] ?? ''));
        preg_match_all('/./us', $word, $characters);
        if (count($characters[0]) === 5) $words[] = $word;
    }
}
?><!doctype html><meta charset="UTF-8"><title>Задание 3</title><h1>Задание 3</h1>
<form method="post"><?php for ($i = 1; $i <= 3; $i++): ?>Слово <?= $i ?>: <input name="word<?= $i ?>" required><br><?php endfor; ?><button>Показать</button></form>
<?php if ($_SERVER['REQUEST_METHOD'] === 'POST'): ?><p>Слова из пяти букв: <?= htmlspecialchars($words ? implode(', ', $words) : 'нет', ENT_QUOTES, 'UTF-8') ?></p><?php endif; ?>
