-- Функция 1: получение среднего балла студента по его id
-- Что делает: Принимает ID студента, возвращает его средний балл по всем экзаменам
-- Логика: SELECT AVG(grade) с фильтром по student_id и где control_type = 'Экзамен'
DROP FUNCTION avg_student_rate;

CREATE OR REPLACE FUNCTION avg_student_rate(input_student_id int, start_year varchar) 
returns decimal(10,2)
LANGUAGE plpgsql
AS $$
	DECLARE avg_rate decimal(10,2);
BEGIN
	SELECT AVG(session_grades.grade) 
	INTO avg_rate
	FROM session_grades
	WHERE session_grades.student_id=input_student_id; AND
	AND substring(session_grades.academic_year from 1 for 4)=start_year
	RETURN avg_rate;
END;
$$;

-- Функция 2: проверка возможности закрытия сессии
-- Что делает: Принимает ID студента и семестр, проверяет, все ли оценки проставлены
-- Логика: Сравнивает количество предметов в учебном плане с количеством оценок у студента
DROP FUNCTION check_session_close;

CREATE OR REPLACE FUNCTION check_session_close(input_student_id int, start_year varchar, semester_num varchar)
returns varchar(200)
LANGUAGE plpgsql
AS $$
	DECLARE session_close varchar(200);
	DECLARE null_count int;
BEGIN
	SELECT COUNT(session_grades)
	INTO null_count
	FROM session_grades
	WHERE session_grades.student_id=input_student_id
	AND substring(session_grades.academic_year from 1 for 4)=start_year
	AND session_grades.semester=semester_num::semester_enum
	AND session_grades.grade is NULL;

	IF null_count>0 THEN session_close='Присутствуют несдачи';
	ELSE session_close='Студент не имеет долгов';
	END IF;

	RETURN session_close;
END;
$$;

-- Функция 3: кол-во студентов по коду группы
-- Что делает: Принимает код группы, возвращает количество учащихся в ней
-- Логика: COUNT(*) из students с JOIN на groups по group_id
DROP FUNCTION student_count_in_group;

CREATE OR REPLACE FUNCTION student_count_in_group(group_code varchar)
returns int
LANGUAGE plpgsql
AS $$
	DECLARE student_count int;
BEGIN
	SELECT count(*) INTO student_count
	FROM students st
	JOIN groups gr on gr.id=st.group_id
	WHERE gr.code=group_code;

	RETURN student_count;
END;
$$;
