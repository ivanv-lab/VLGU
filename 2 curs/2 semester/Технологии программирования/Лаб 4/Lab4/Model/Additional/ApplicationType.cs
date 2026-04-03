using Lab4.Model.Main;

namespace Lab4.Model.Additional
{
    public class ApplicationType
    {
        public long id {  get; set; }
        public string name { get; set; }

        public ICollection<Application> applications {  get; set; }

        public ApplicationType(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
