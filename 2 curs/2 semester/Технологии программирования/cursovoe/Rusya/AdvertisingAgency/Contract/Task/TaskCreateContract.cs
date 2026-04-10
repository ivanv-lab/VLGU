namespace AdvertisingAgency.Contract.Task;

public class TaskCreateContract
{
    public string title { get; set; }
    public string body { get; set; }
    public DateOnly deadline { get; set; }

    public int statusId { get; set; }
    public string assignedUserId { get; set; }
    public long campaignId { get; set; }

    public TaskCreateContract(string title, string body, DateOnly deadline, int statusId, string assignedUserId, long campaignId)
    {
        this.title = title;
        this.body = body;
        this.deadline = deadline;
        this.statusId = statusId;
        this.assignedUserId = assignedUserId;
        this.campaignId = campaignId;
    }
}