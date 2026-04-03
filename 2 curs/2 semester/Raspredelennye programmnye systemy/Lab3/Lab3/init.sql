create table users(
id serial primary key,
name varchar(255),
created_date timestamp,
updated_date timestamp
);

create table tasks(
id serial primary key,
name varchar(255),
text text,
created_date timestamp,
updated_date timestamp,
user_id serial references users(id)
);