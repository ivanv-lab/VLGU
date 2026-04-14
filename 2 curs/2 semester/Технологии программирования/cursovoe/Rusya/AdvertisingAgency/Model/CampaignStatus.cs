using System.Text.Json.Serialization;

namespace AdvertisingAgency.Model
{
    public class CampaignStatus
    {
        public int id { get; set; }
        public string name { get; set; }

        [JsonIgnore]
        public ICollection<AdvertisingCampaign> campaigns { get; set; }

        public CampaignStatus(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
