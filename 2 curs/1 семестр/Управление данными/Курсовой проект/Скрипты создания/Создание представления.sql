-- Представление: Сводная ведомость успеваемости студентов с детальной информацией
CREATE VIEW student_performance_report AS
SELECT 
    s.id AS student_id,
    s.full_name AS student_name,
    s.birth_date,
    EXTRACT(YEAR FROM age(s.birth_date)) AS age,

    g.code AS group_code,
    spec.name AS specialty_name,

    sub.name AS subject_name,
    sub.hours AS subject_hours,
    sub.control_type,

    sg.academic_year,
    sg.semester,
    sg.grade,

    CASE 
        WHEN sg.grade IS NULL THEN 'Не сдано'
        WHEN sg.grade BETWEEN 1 AND 2 THEN 'Неудовлетворительно'
        WHEN sg.grade = 3 THEN 'Удовлетворительно'
        WHEN sg.grade = 4 THEN 'Хорошо'
        WHEN sg.grade = 5 THEN 'Отлично'
        ELSE 'Не определено'
    END AS grade_description,
    
    CASE 
        WHEN sg.grade IS NULL THEN 'Не сдан'
        WHEN sub.control_type = 'Зачет' AND sg.grade >= 3 THEN 'Зачет'
        WHEN sub.control_type = 'Экзамен' AND sg.grade >= 3 THEN 'Сдан'
        ELSE 'Не сдан'
    END AS exam_status

FROM students s
JOIN groups g ON s.group_id = g.id
JOIN specialties spec ON g.specialty_id = spec.id
JOIN session_grades sg ON s.id = sg.student_id
JOIN subjects sub ON sg.subject_id = sub.id
ORDER BY s.full_name, sg.academic_year DESC, sg.semester DESC, sub.name;