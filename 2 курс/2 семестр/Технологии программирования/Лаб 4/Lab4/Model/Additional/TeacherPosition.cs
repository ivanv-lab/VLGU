using Lab4.Model.Main;

namespace Lab4.Model.Additional
{
    public class TeacherPosition
    {
        public long id { get; set; }
        public string name { get; set; }

        public ICollection<Teacher> teachers   { get; set; }

        public TeacherPosition(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
