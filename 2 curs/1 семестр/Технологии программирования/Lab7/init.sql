CREATE TABLE groups(
id SERIAL PRIMARY KEY,
name VARCHAR(50) NOT NULL,
description VARCHAR(50) NOT NULL
);

CREATE TABLE contacts(
id SERIAL PRIMARY KEY,
group_id SERIAL,
first_name VARCHAR(50) NOT NULL,
second_name VARCHAR(50) NOT NULL,
phone VARCHAR(50) NOT NULL,
email VARCHAR(255) NOT NULL,
FOREIGN KEY (group_id) REFERENCES groups(id) ON DELETE CASCADE
);

INSERT INTO groups (name, description) VALUES
('Семья', 'Близкие родственники'),
('Друзья', 'Близкие друзья'),
('Коллеги', 'Рабочие контакты');

INSERT INTO contacts (group_id, first_name, second_name, phone, email) VALUES
(1, 'Иван', 'Иванов', '+7-999-123-45-67', 'ivan.ivanov@example.com'),
(1, 'Мария', 'Петрова', '+7-999-123-45-68', 'maria.petrova@example.com'),
(2, 'Алексей', 'Сидоров', '+7-999-123-45-69', 'alexey.sidorov@example.com'),
(2, 'Елена', 'Кузнецова', '+7-999-123-45-70', 'elena.kuznetsova@example.com'),
(2, 'Дмитрий', 'Смирнов', '+7-999-123-45-71', 'dmitry.smirnov@example.com'),
(3, 'Ольга', 'Васильева', '+7-999-123-45-72', 'olga.vasileva@example.com'),
(3, 'Сергей', 'Попов', '+7-999-123-45-73', 'sergey.popov@example.com'),
(1, 'Анна', 'Новикова', '+7-999-123-45-74', 'anna.novikova@example.com'),
(2, 'Павел', 'Морозов', '+7-999-123-45-75', 'pavel.morozov@example.com'),
(3, 'Наталья', 'Волкова', '+7-999-123-45-76', 'natalya.volkova@example.com');