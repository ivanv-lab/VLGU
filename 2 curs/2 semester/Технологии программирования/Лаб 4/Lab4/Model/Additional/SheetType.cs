using Lab4.Model.Main;

namespace Lab4.Model.Additional
{
    public class SheetType
    {
        public int id {  get; set; }
        public string name { get; set; }

        public ICollection<GradeSheet> gradeSheets { get; set; }

        public SheetType(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
