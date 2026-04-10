namespace AdvertisingAgency.Contract.CampaignStatus;

public class CampaignStatusCreateContract
{
    public string name { get; set; }

    public CampaignStatusCreateContract(string name)
    {
        this.name = name;
    }
}