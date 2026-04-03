using Lab4.Model.Additional;

namespace Lab4.Model.Main
{
    public class Discipline
    {
        public long id { get; set; }
        public string name { get; set; }
        public int hours { get; set; }
        public int controlFormId {  get; set; }

        public ControlForm controlForm { get; set; }
        public ICollection<GradeSheet> gradeSheets { get; set; }
    }
}
