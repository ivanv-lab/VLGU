<?php
$file = __DIR__ . '/student_records.json'; $records = is_file($file) ? json_decode((string)file_get_contents($file), true) : [];
if (!$records) $records = [
    ['number' => 1, 'name' => 'Иванов Иван Иванович', 'birthYear' => 2005, 'address' => 'г. Владимир, ул. Лесная, 10'],
    ['number' => 2, 'name' => 'Петрова Анна Сергеевна', 'birthYear' => 2004, 'address' => 'г. Владимир, ул. Центральная, 5'],
    ['number' => 3, 'name' => 'Сидоров Максим Олегович', 'birthYear' => 2005, 'address' => 'г. Ковров, ул. Молодёжная, 8'],
    ['number' => 4, 'name' => 'Кузнецова Мария Андреевна', 'birthYear' => 2006, 'address' => 'г. Суздаль, ул. Школьная, 2'],
    ['number' => 5, 'name' => 'Волков Дмитрий Павлович', 'birthYear' => 2004, 'address' => 'г. Владимир, ул. Спортивная, 14'],
];
if ($_SERVER['REQUEST_METHOD'] === 'POST') { $show = array_filter(array_map('intval', explode(',', (string)($_POST['show'] ?? '')))); $delete = array_filter(array_map('intval', explode(',', (string)($_POST['delete'] ?? '')))); $display = $show ? array_values(array_filter($records, fn($r) => in_array($r['number'], $show, true))) : $records; $records = array_values(array_filter($records, fn($r) => !in_array($r['number'], $delete, true))); foreach ($records as $i => &$record) $record['number'] = $i + 1; unset($record); file_put_contents($file, json_encode($records, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE)); }
?><!doctype html><meta charset="UTF-8"><title>Задание 11</title><h1>Задание 11</h1>
<p>Поля записи: № п/п, Ф.И.О., год рождения, домашний адрес.</p><form method="post"><p>Показать номера через запятую (пусто — все): <input name="show" placeholder="1, 3"></p><p>Удалить номера через запятую: <input name="delete" placeholder="2, 4"></p><button>Обработать</button></form>
<table border="1"><tr><th>№</th><th>Ф.И.О.</th><th>Год рождения</th><th>Адрес</th></tr><?php foreach (($display ?? $records) as $record): ?><tr><td><?= $record['number'] ?></td><td><?= htmlspecialchars($record['name'], ENT_QUOTES, 'UTF-8') ?></td><td><?= $record['birthYear'] ?></td><td><?= htmlspecialchars($record['address'], ENT_QUOTES, 'UTF-8') ?></td></tr><?php endforeach; ?></table>
