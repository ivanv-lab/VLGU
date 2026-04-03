using Lab4.Model.Main;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Model.Additional
{
    public class ApplicationStatus
    {
        public long id { get; set; }
        public string name { get; set;  }

        public ICollection<Application> applications { get; set; }

        public ApplicationStatus(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
