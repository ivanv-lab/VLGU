namespace AdvertisingApplication.Contract.AdvertisingCampaign;

public class AdvertisingCampaignCreateContract
{
    public string name { get; set; }
    public string description { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly endDate { get; set; }
    public decimal budget { get; set; }
    public int statusId { get; set; }
    public int categoryId {  get; set; }
    public long clientId {  get; set; }

    public AdvertisingCampaignCreateContract(string name, string description, DateOnly startDate, DateOnly endDate, decimal budget, int statusId, int categoryId, long clientId)
    {
        this.name = name;
        this.description = description;
        this.startDate = startDate;
        this.endDate = endDate;
        this.budget = budget;
        this.statusId = statusId;
        this.categoryId = categoryId;
        this.clientId = clientId;
    }
}