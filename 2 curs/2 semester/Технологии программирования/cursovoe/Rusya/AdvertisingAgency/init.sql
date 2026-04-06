create table roles(
id integer primary key,
name varchar(255) unique
);

create table users(
id serial primary key,
fullname varchar(255),
email varchar(255) unique,
passwordHash varchar(255),
role_id integer references roles(id)
);

create table clients(
id serial primary key,
name varchar(255),
contact_person_fullname varchar(255),
email varchar(255) unique,
phone varchar(11) unique,
address varchar(255)
);

create table campaig_statuses(
id integer primary key,
name varchar(255) unique
);

create table campaign_categories(
id integer primary key,
name varchar(255) unique
);

create table advertising_campaigns(
id serial primary key,
name varchar(255) unique,
description text,
startDate date,
endDate date,
decimal bugdet,
status_id integer references campaig_statuses(id),
category_id integer references campaign_categories(id),
client_id serial references clients(id),
);

create table task_statuses(
id integer primary key,
name varchar(255) unique
);

create table tasks(
id serial primary key,
title varchar(255),
body text,
deadline date,
status_id integer references task_statuses(id),
assigned_user_id serial references users(id),
campaig_id serial references advertising_campaigns(id)
);