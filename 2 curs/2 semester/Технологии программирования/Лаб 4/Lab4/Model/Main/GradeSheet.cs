using Lab4.Model.Additional;

namespace Lab4.Model.Main
{
    public class GradeSheet
    {
        public long id { get; set; }
        public int semester { get; set; }
        public DateOnly dateOpened { get; set; }
        public DateOnly? dateClosed { get; set; }

        public int sheetTypeId { get; set; }
        public int academicRecordId { get; set; }
        public int disciplineId { get; set; }
        public int teacherId { get; set; }

        public SheetType sheetType { get; set; }
        public AcademicRecord academicRecord { get; set; }
        public Discipline discipline { get; set; }
        public Teacher teacher { get; set; }
    }
}
