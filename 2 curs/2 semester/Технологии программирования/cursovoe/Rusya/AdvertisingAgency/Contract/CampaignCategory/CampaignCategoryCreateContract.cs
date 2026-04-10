namespace AdvertisingAgency.Contract.CampaignCategory;

public class CampaignCategoryCreateContract
{
    public string name { get; set; }

    public CampaignCategoryCreateContract(string name)
    {
        this.name = name;
    }
}