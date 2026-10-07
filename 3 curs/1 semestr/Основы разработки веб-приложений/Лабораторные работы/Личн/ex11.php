<?php
$file = __DIR__ . '/school_records.json';
$records = is_file($file) ? json_decode((string)file_get_contents($file), true) : [];
if (!$records) {
    $records = [
        ['number' => 18, 'director' => 'Иванова Елена Сергеевна', 'classes' => 24, 'students' => 612, 'teachers' => 48, 'rooms' => 32],
        ['number' => 7, 'director' => 'Петров Александр Викторович', 'classes' => 18, 'students' => 430, 'teachers' => 36, 'rooms' => 25],
        ['number' => 31, 'director' => 'Смирнова Ольга Андреевна', 'classes' => 30, 'students' => 790, 'teachers' => 61, 'rooms' => 41],
        ['number' => 2, 'director' => 'Кузнецов Дмитрий Игоревич', 'classes' => 12, 'students' => 275, 'teachers' => 27, 'rooms' => 19],
        ['number' => 24, 'director' => 'Волкова Марина Павловна', 'classes' => 22, 'students' => 548, 'teachers' => 44, 'rooms' => 29],
    ];
}
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $records = json_decode((string)($_POST['records'] ?? '[]'), true) ?: [];
    usort($records, fn($x, $y) => (int)$x['number'] <=> (int)$y['number']);
    $updates = json_decode((string)($_POST['updates'] ?? '{}'), true) ?: [];
    foreach ($records as &$record) if (array_key_exists((string)$record['number'], $updates)) $record['teachers'] = (int)$updates[(string)$record['number']]; unset($record);
    file_put_contents($file, json_encode($records, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE));
}
?><!doctype html><meta charset="UTF-8"><title>Задание 11</title><h1>Задание 11</h1>
<p>Записи сортируются по номеру школы и сохраняются в <code>school_records.json</code>.</p>
<form method="post"><p>Введите JSON-массив записей с полями number, director, classes, students, teachers, rooms:</p><textarea name="records" rows="8" cols="100" required><?= htmlspecialchars(json_encode($records, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE), ENT_QUOTES, 'UTF-8') ?></textarea><p>Изменения учителей: JSON-объект вида {"12": 40}</p><input name="updates" value="{}" size="50"><br><button>Сохранить</button></form>
<?php if ($records): ?><pre><?= htmlspecialchars(json_encode($records, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE), ENT_QUOTES, 'UTF-8') ?></pre><?php endif; ?>
