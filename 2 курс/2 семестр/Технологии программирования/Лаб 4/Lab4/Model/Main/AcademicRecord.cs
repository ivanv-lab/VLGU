namespace Lab4.Model.Main
{
    public class AcademicRecord
    {
        public long id {  get; set; }
        public int? grade { get; set; }
        public bool isPassed { get; set; }
        public int studentId { get; set; }

        public Student student { get; set; }
        public ICollection<GradeSheet> gradeSheets { get; set; }
    }
}
