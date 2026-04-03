namespace Lab6.Models.ViewModels;

public class CreateTaskViewModel
{
    public Tasks task { get; set; }
    public List<AdvertisingCampaign> campaigns { get; set; }
    public List<TaskStatuses> statuses { get; set; }
    public AdvertisingCampaign campaign { get; set; }
    public TaskStatuses status { get; set; }

    public CreateTaskViewModel()
    {
    }

    public CreateTaskViewModel(Tasks task, List<AdvertisingCampaign> campaigns, List<TaskStatuses> statuses, AdvertisingCampaign campaign, TaskStatuses status)
    {
        this.task = task;
        this.campaigns = campaigns;
        this.statuses = statuses;
        this.campaign = campaign;
        this.status = status;
    }
}