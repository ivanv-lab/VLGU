using System.Text.Json.Serialization;

namespace AdvertisingAgency.Model
{
    public class TaskStatus
    {
        public int id { get; set; }
        public string name { get; set; }

        [JsonIgnore]
        public ICollection<Task> tasks { get; set; }

        public TaskStatus(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
