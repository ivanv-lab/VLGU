namespace AdvertisingAgency.Model
{
    public class CampaignStatus
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<AdvertisingCampaign> campaigns { get; set; }
    }
}
