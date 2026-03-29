namespace Lab4.Model.Main
{
    public class Curriculum
    {
        public long id { get; set; }
        public int semester { get; set; }
        public int year { get; set; }
        public int teacherId {  get; set; }

        public Teacher teacher { get; set; }
        public Group group { get; set; }
    }
}
