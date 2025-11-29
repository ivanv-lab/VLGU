CREATE TABLE specialties
(
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE groups
(
    id SERIAL PRIMARY KEY,
    code VARCHAR(20),
    specialty_id serial references specialties (id)
);

CREATE TABLE students 
(
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(255),
    birth_date DATE,
    address TEXT,
    phone VARCHAR(11),
	biography TEXT,
	group_id serial references groups (id)
);

CREATE TYPE control_enum AS ENUM('Экзамен','Зачет');

CREATE TABLE subjects 
(
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    hours INT,
    control_type control_enum
);

CREATE TYPE semester_enum AS ENUM('1','2');

CREATE TABLE session_grades 
(
    id SERIAL PRIMARY KEY,
    student_id serial references students(id),
    subject_id serial references subjects(id),
    academic_year VARCHAR(9),
    semester semester_enum,
	grade INT
);