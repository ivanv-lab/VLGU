namespace AdvertisingAgency.Model
{
    public class CampaignCategory
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<AdvertisingCampaign> campaigns { get; set; }
    }
}
