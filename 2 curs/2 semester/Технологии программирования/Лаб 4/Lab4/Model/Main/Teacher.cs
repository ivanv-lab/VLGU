using Lab4.Model.Additional;

namespace Lab4.Model.Main
{
    public class Teacher
    {
        public long id { get; set; }
        public string fullname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string passportSerial { get; set; }
        public string passportNumber { get; set; }
        public string passportRegistration { get; set; }
        public string passportIssued { get; set; }
        public string departament { get; set; }
        public int teacherPositionId { get; set; }

        public TeacherPosition teacherPosition { get; set; }
        public ICollection<Curriculum> curriculums { get; set; }
        public ICollection<GradeSheet> gradeSheets { get; set; }
    }
}
