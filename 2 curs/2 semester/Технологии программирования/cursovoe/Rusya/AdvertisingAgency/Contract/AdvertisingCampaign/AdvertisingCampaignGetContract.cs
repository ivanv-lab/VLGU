namespace AdvertisingAgency.Contract.AdvertisingCampaign
{
    public class AdvertisingCampaignGetContract
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate {  get; set; }
        public decimal budget { get; set; }
        public string statusName { get; set; }
        public string categoryName { get; set; }
        public string clientName { get; set; }
        public List<string> taskNames { get; set; }

        public AdvertisingCampaignGetContract(long id, string name, string description,
            DateOnly startDate, DateOnly endDate,
            decimal budget, string statusName, string categoryName, string clientName,
            List<string> taskNames)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.startDate = startDate;
            this.endDate = endDate;
            this.budget = budget;
            this.statusName = statusName;
            this.categoryName = categoryName;
            this.clientName = clientName;
            this.taskNames = taskNames;
        }
    }
}
