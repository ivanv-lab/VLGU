-- Процедура 1: Процедура добавления нового студента
-- Что делает: Принимает все данные студента и добавляет запись в таблицу students
-- Логика: INSERT с проверкой существования группы
DROP PROCEDURE create_student;

CREATE OR REPLACE PROCEDURE 
create_student(
last_name varchar(50),
first_name varchar(100),
second_name varchar(100),
birth_date date, 
address text,
phone varchar(11),
biography text,
group_code varchar(9)) 

LANGUAGE plpgsql
AS $$
	DECLARE group_exists boolean;
	group_id int;
BEGIN
	SELECT EXISTS(SELECT 1 FROM groups WHERE
	groups.code=group_code) INTO group_exists;

	if not group_exists then
	raise exception 'Группа с кодом % не существует', group_code;
	end if;

	SELECT groups.id from groups
	where groups.code=group_code INTO group_id;

	INSERT into students(full_name, birth_date, address,
	phone, biography, group_id) VALUES 
	(last_name || ' ' || first_name || ' ' || second_name,
	birth_date, address, phone, biography, group_id);
END;
$$;

-- Процедура 2: Процедура обновления оценок за сессию
-- Что делает: Принимает ID студента, предмета, семестр и новую оценку, обновляет запись
-- Логика: UPDATE с проверкой существования студента и предмета
DROP PROCEDURE update_session_grades;

CREATE OR REPLACE PROCEDURE 
update_session_grades(
student_name varchar(255),
subject_name varchar(255),
semester_num semester_enum,
new_grade int) 

LANGUAGE plpgsql
AS $$
	DECLARE is_exists boolean;
	finding_student_id int;
	finding_subject_id int;
	current_year varchar(4);
BEGIN
	SELECT EXISTS(SELECT 1 FROM students WHERE
	students.full_name=student_name) INTO is_exists;

	if not is_exists then
	raise exception 'Студент с именем % не существует', student_name;
	end if;

	SELECT EXISTS(SELECT 1 FROM subjects WHERE
	subjects.name=subject_name) INTO is_exists;

	if not is_exists then
	raise exception 'Предмет с названием % не существует', subject_name;
	end if;

	if new_grade <=0 OR new_grade >5 then
	raise exception 'Оценка % не может быть меньше нуля и больше 5', new_grade;
	end if;

	SELECT id FROM students WHERE
	students.full_name=student_name into finding_student_id;

	SELECT id FROM subjects WHERE
	subjects.name=subject_name INTO finding_subject_id;

	SELECT to_char(NOW(), 'YYYY') into current_year;

	SELECT EXISTS(
		SELECT 1 from session_grades sg
		WHERE sg.student_id=finding_student_id
		AND sg.subject_id=finding_subject_id
		AND substring(sg.academic_year from 1 for 4)=current_year 
		OR substring(sg.academic_year from 6 for 9)=current_year
		AND sg.semester=semester_num
	) into is_exists;

	if not is_exists then
	raise exception 'Запись о сессионной успеваемости для студента % отсутствует', student_name;
	end if;

	UPDATE session_grades sg
	SET grade=new_grade
	WHERE sg.student_id=finding_student_id
	AND sg.subject_id=finding_subject_id
	AND substring(sg.academic_year from 1 for 4)=current_year
	OR substring(sg.academic_year from 6 for 9)=current_year
	AND sg.semester=semester_num;
END;
$$;

-- Процедура 3: Процедура перевода студента в другую группу
-- Что делает: Принимает ID студента и ID новой группы, обновляет его группу
-- Логика: UPDATE students SET group_id = ... с проверкой существования группы
DROP PROCEDURE student_replace_group;

CREATE OR REPLACE PROCEDURE student_replace_group(
student_name varchar(255),
group_code varchar(20))
LANGUAGE plpgsql
AS $$
	DECLARE is_exists boolean;
	finding_student_id int;
	finding_group_id int;
BEGIN
	SELECT EXISTS(SELECT 1 FROM students WHERE
	students.full_name=student_name) INTO is_exists;

	if not is_exists then
	raise exception 'Студент с именем % не существует', student_name;
	end if;

	SELECT EXISTS(SELECT 1 FROM groups WHERE
	groups.code=group_code) INTO is_exists;

	if not is_exists then
	raise exception 'Группа с кодом % не существует', group_code;
	end if;

	SELECT groups.id from groups
	where groups.code=group_code INTO finding_group_id;

	SELECT id FROM students WHERE
	students.full_name=student_name into finding_student_id;

	UPDATE students st
	SET group_id=finding_group_id
	WHERE st.id=finding_student_id;
END;
$$;