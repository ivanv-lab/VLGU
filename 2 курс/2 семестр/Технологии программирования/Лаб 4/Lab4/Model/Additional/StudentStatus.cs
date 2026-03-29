using Lab4.Model.Main;

namespace Lab4.Model.Additional
{
    public class StudentStatus
    {
        public long id { get; set; }
        public string name { get; set; }

        public ICollection<Student> students { get; set; }

        public StudentStatus(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
