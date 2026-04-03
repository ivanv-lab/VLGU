create table application_statuses(
id serial primary key,
name varchar(255) unique
);

create table application_types(
id serial primary key,
name varchar(255) unique
);

create table control_forms(
id serial primary key,
name varchar(255) unique
);

create table dean_posts(
id serial primary key,
name varchar(255) unique
);

create table sheet_types(
id serial primary key,
name varchar(255) unique
);

create table spetialties(
id serial primary key,
name varchar(255) unique,
code varchar(255) unique
);

create table student_statuses(
id serial primary key,
name varchar(255) unique
);

create table teacher_positions(
id serial primary key,
name varchar(255) unique
);

create table students(
id serial primary key,
fullname varchar(255),
birthdate date,
phone varchar(11),
email varchar(200),
passport_serial varchar(6),
passport_number varchar(4),
passport_registration varchar(255),
passport_issued varchar(255),
record_book_number varchar(100),
enrollment_date date,
status_id integer references student_statuses(id)
);

create table academic_records(
id serial primary key,
grade integer,
is_passed boolean,
student_id integer references students(id)
);

create table deanery_staff(
id serial primary key,
fullname varchar(255),
birthdate date,
phone varchar(11),
email varchar(200),
passport_serial varchar(6),
passport_number varchar(4),
passport_registration varchar(255),
passport_issued varchar(255),
dean_post_id integer references dean_posts(id)
);

create table applications(
id serial primary key,
name varchar(255),
application_type_id integer references application_types(id),
application_status_id integer references application_statuses(id),
submission_date date,
deanery_staff_id integer references deanery_staff(id)
);

create table orders(
id serial primary key,
order_number varchar(255),
order_date date,
content_file_path varchar(255),
application_id serial references applications(id)
);

create table teachers(
id serial primary key,
fullname varchar(255),
birthdate date,
phone varchar(11),
email varchar(200),
passport_serial varchar(6),
passport_number varchar(4),
passport_registration varchar(255),
passport_issued varchar(255),
departament varchar(255),
teacher_position_id integer references teacher_positions(id)
);

create table curriculums(
id serial primary key,
semester integer,
year integer,
teacher_id serial references teachers(id)
);

create table groups(
id serial primary key,
group_code varchar(255),
year_of_entry integer,
spetialty_id integer references spetialties(id),
curriculun_id integer references curriculums(id)
);

create table disciplines(
id serial primary key,
name varchar(255),
hours integer,
control_form_id integer references control_forms(id)
);

create table grade_sheets(
id serial primary key,
sheet_type_id integer references sheet_types(id),
semester integer,
date_opened date,
date_closed date,
academic_record_id serial references academic_records(id),
discipline_id serial references disciplines(id),
teacher_id integer references teachers(id)
);