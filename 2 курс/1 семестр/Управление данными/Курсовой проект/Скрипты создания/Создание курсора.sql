-- Курсор: Расчет стипендии студентов на основе успеваемости
-- Функционал: Проходит по всем студентам группы и рассчитывает размер стипендии
-- в зависимости от среднего балла за последнюю сессию
DROP FUNCTION calculate_scholarship_for_group;

CREATE OR REPLACE FUNCTION calculate_scholarship_for_group(group_code_param VARCHAR(20))
RETURNS TABLE(
    s_id int,
    s_name varchar(255),
    g_code varchar(20),
    avg_grade numeric,
    scholarship_amt numeric,
    scholarship_type_desc varchar(50)
) AS $$
DECLARE
    student_rec record;
    subject_rec record;
    total_grades numeric;
    subjects_count int;
    current_avg numeric;
    scholarship_val numeric;
    scholarship_desc varchar(50);
    
    student_cur CURSOR FOR
        SELECT s.id, s.full_name, g.code as group_code
        FROM students s
        JOIN groups g ON s.group_id = g.id
        WHERE g.code = group_code_param;
        
BEGIN
    RAISE NOTICE 'Начинаем расчет стипендии для группы: %', group_code_param;
    
    OPEN student_cur;
    
    LOOP
        FETCH student_cur INTO student_rec;
        EXIT WHEN NOT FOUND;
        
        RAISE NOTICE 'Обрабатываем студента: %', student_rec.full_name;
        
        FOR subject_rec IN 
            SELECT sg.grade, sub.name
            FROM session_grades sg
            JOIN subjects sub ON sg.subject_id = sub.id
            WHERE sg.student_id = student_rec.id
            AND sg.academic_year = (
                SELECT MAX(academic_year) 
                FROM session_grades 
                WHERE student_id = student_rec.id
            )
            AND sg.semester = (
                SELECT MAX(semester) 
                FROM session_grades 
                WHERE student_id = student_rec.id
                AND academic_year = (
                    SELECT MAX(academic_year) 
                    FROM session_grades 
                    WHERE student_id = student_rec.id
                )
            )
            AND sg.grade IS NOT NULL
        LOOP
            total_grades = total_grades + subject_rec.grade;
            subjects_count = subjects_count + 1;
            RAISE NOTICE '  Предмет: %, Оценка: %', subject_rec.name, subject_rec.grade;
        END LOOP;

        IF subjects_count > 0 THEN
            current_avg = ROUND(total_grades / subjects_count, 2);
        ELSE
            current_avg = 0;
        END IF;
        
        IF current_avg >= 4.5 AND subjects_count >= 3 THEN
            scholarship_val = 2000.00;
            scholarship_desc = 'Повышенная';
        ELSIF current_avg >= 4.0 AND subjects_count >= 3 THEN
            scholarship_val = 1500.00;
            scholarship_desc = 'Обычная';
        ELSIF current_avg >= 3.5 AND subjects_count >= 3 THEN
            scholarship_val = 1000.00;
            scholarship_desc = 'Минимальная';
        ELSE
            scholarship_val := 0.00;
            scholarship_desc := 'Без стипендии';
        END IF;

        s_id := student_rec.id;
        s_name := student_rec.full_name;
        g_code := student_rec.group_code;
        avg_grade := current_avg;
        scholarship_amt := scholarship_val;
        scholarship_type_desc := scholarship_desc;
        
        RETURN NEXT;
        
        RAISE NOTICE 'Студент: %, Средний балл: %, Стипендия: % (%)', 
                     student_rec.full_name, current_avg, scholarship_val, scholarship_desc;
    END LOOP;
    
    CLOSE student_cur;
    
    RAISE NOTICE 'Расчет стипендии для группы % завершен', group_code_param;
    
END;
$$ LANGUAGE plpgsql;