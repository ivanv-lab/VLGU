namespace AdvertisingApplication.Model
{
    public class CampaignCategory
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<AdvertisingCampaign> campaigns { get; set; }

        public CampaignCategory(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
