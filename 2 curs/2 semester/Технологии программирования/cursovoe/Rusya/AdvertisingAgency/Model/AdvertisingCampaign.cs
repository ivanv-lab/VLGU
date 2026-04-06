namespace AdvertisingAgency.Model
{
    public class AdvertisingCampaign
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public decimal budget { get; set; }
        public int statusId { get; set; }
        public int categoryId {  get; set; }
        public long clientId {  get; set; }

        public Client client { get; set; }
        public CampaignStatus status { get; set; }
        public CampaignCategory category { get; set; }
        public ICollection<Task> tasks { get; set; }
    }
}
