create table roles(
id varchar(255) primary key,
name varchar(255) unique,
normalized_name varchar(255) unique,
concurrency_stamp varchar(255)
);

create table users(
id varchar(255) primary key,
fullname varchar(255),
normalized_username varchar(255),
email varchar(255) unique,
normalized_email varchar(255),
email_confirmed boolean,
password_hash varchar(255),
security_stamp varchar(255),
concurrency_stamp varchar(255),
phone_number varchar(11),
phone_number_confirmed boolean,
two_factor_enabled boolean,
lockout_end timestamp,
lockout_enabled boolean,
access_failed_count integer,
role_id varchar(255) references roles(id)
);

create table clients(
id serial primary key,
name varchar(255) unique,
contact_person_fullname varchar(255),
email varchar(255),
phone varchar(11),
address varchar(255)
);

create table campaign_statuses(
id serial primary key,
name varchar(255) unique
);

create table campaign_categories(
id serial primary key,
name varchar(255) unique
);

create table advertising_campaigns(
id serial primary key,
name varchar(255) unique,
description text,
start_date date,
end_date date,
budget decimal,
status_id integer references campaign_statuses(id),
category_id integer references campaign_categories(id),
client_id serial references clients(id)
);

create table task_statuses(
id serial primary key,
name varchar(255) unique
);

create table tasks(
id serial primary key,
title varchar(255),
body text,
deadline date,
status_id integer references task_statuses(id),
assigned_user_id varchar(255) references users(id),
campaign_id serial references advertising_campaigns(id)
);

insert into campaign_statuses(name) 
values ('Завершено'), ('Активно'),
('Неактивно'),('Отложено'),('Заморожено');

insert into campaign_categories(name)
values ('Имиджевая (брендинговая)'), ('Продуктовая (товарная)'),
('Стимулирующая (торговая)'), ('Социальная (PR)');

insert into task_statuses(name)
values ('Завершено'), ('Активно'), 
('Неактивно'), ('Отложено'), ('Заморожено');