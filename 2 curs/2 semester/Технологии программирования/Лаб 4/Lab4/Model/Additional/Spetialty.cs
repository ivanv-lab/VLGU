using Lab4.Model.Main;

namespace Lab4.Model.Additional
{
    public class Spetialty
    {
        public long id {  get; set; }
        public string name { get; set; }
        public string code { get; set; }

        public ICollection<Group> groups { get; set; }

        public Spetialty(long id, string name, string code)
        {
            this.id = id;
            this.name = name;
            this.code = code;
        }
    }
}
