namespace Lab5.Models.ViewModels;

public class CreateCampaignViewModel
{
    public AdvertisingCampaign campaign { get; set; }
    public List<CampaignStatuses> statuses { get; set; }
    public CampaignStatuses status { get; set; }
    public List<Client> clients { get; set; }
    public Client client { get; set; }

    public CreateCampaignViewModel()
    {
    }

    public CreateCampaignViewModel(AdvertisingCampaign campaign, List<CampaignStatuses> statuses, CampaignStatuses status, List<Client> clients, Client client)
    {
        this.campaign = campaign;
        this.statuses = statuses;
        this.status = status;
        this.clients = clients;
        this.client = client;
    }
}