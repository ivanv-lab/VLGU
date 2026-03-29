using Lab4.Model.Main;

namespace Lab4.Model.Additional
{
    public class ControlForm
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Discipline> disciplines { get; set; }

        public ControlForm(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
