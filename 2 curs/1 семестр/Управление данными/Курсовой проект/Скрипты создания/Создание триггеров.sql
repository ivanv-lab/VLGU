-- Триггер валидации и автоматического исправления телефона
-- Таблица: students
-- Событие: BEFORE INSERT OR UPDATE
-- Логика:
-- Ветвление: Проверяет формат телефона (IF)
-- Цикл: Если телефон содержит нецифровые символы - проходит по каждому символу и оставляет только цифры
-- Автоматически приводит телефон к формату 7XXXXXXXXXX
-- Если после очистки длина ≠ 11 символов - отменяет операцию
CREATE OR REPLACE FUNCTION validate_phone()
RETURNS TRIGGER AS $$
DECLARE
	cleaned_phone text;
	char_record char;
	counter int;
BEGIN
	if new.phone is null then
	return new;
	end if;

	if new.phone!~'^[0-9]+$' then
	for char_record in select regexp(new.phone,'')
	loop 
		if char_record ~ '[0-9]' then
		cleaned_phone=cleaned_phone||char_record;
		counter=counter+1;

		if counter>11 then
		exit;
		end if;
		end if;
	end loop;
	else cleaned_phone=new.phone;
	end if;

	if length(cleaned_phone)=11 then
		if substring(cleaned_phone from 1 for 1)='8'
		then new.phone=7||substring(cleaned_phone from 2);
		elsif substring(cleaned_phone from 1 for 1)='7'then
		new.phone=cleaned_phone;
		else
		raise exception 'Номер телефона должен начинаться с 7 или 8. Получен: %', cleaned_phone;
	end if;
	else 
	RAISE EXCEPTION 'Номер телефона должен содержать 11 цифр после очистки. Получено: % цифр (%)', 
                       LENGTH(cleaned_phone), cleaned_phone;
    END IF;

	return new;
end;
$$ LANGUAGE plpgsql;

CREATE TRIGGER phone_validation_trigger
    BEFORE INSERT OR UPDATE ON students
    FOR EACH ROW
    EXECUTE FUNCTION validate_phone();



-- Триггер: Автоматическое создание ведомости для всех студентов группы при добавлении нового предмета в сессию
-- Функционал: Когда добавляется запись о новом предмете в session_grades для одного студента,
-- автоматически создаются такие же записи для всех студентов его группы
CREATE OR REPLACE FUNCTION create_group_grade_records()
RETURNS TRIGGER AS $$
DECLARE
    student_group_id int;
    group_student record;
    existing_record boolean;
    created_count int;
BEGIN
    IF TG_OP = 'INSERT' THEN
        SELECT group_id INTO student_group_id 
        FROM students 
        WHERE id = NEW.student_id;
        
        RAISE NOTICE 'Добавлен предмет для студента ID: %. Группа: %', NEW.student_id, student_group_id;

        FOR group_student IN 
            SELECT id 
            FROM students 
            WHERE group_id = student_group_id 
            AND id != NEW.student_id
        LOOP
            SELECT EXISTS (
                SELECT 1 FROM session_grades 
                WHERE student_id = group_student.id 
                AND subject_id = NEW.subject_id 
                AND academic_year = NEW.academic_year 
                AND semester = NEW.semester
            ) INTO existing_record;
            
            IF NOT existing_record THEN
                INSERT INTO session_grades 
                (student_id, subject_id, academic_year, semester, grade)
                VALUES 
                (group_student.id, NEW.subject_id, NEW.academic_year, NEW.semester, NULL);
                
                created_count := created_count + 1;
                RAISE NOTICE 'Создана запись для студента ID: %', group_student.id;
            ELSE
                RAISE NOTICE 'Запись для студента ID: % уже существует', group_student.id;
            END IF;
        END LOOP;
        
        RAISE NOTICE 'Создано % новых записей в ведомости для группы %', created_count, student_group_id;
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER create_group_grade_trigger
    AFTER INSERT ON session_grades
    FOR EACH ROW
    EXECUTE FUNCTION create_group_grade_records();