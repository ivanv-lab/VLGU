using AdvertisingApplication.Model.Authorization;

namespace AdvertisingApplication.Model
{
    public class Task
    {
        public long id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateOnly deadline { get; set; }

        public int statusId { get; set; }
        public string assignedUserId { get; set; }
        public long campaignId { get; set; }

        public TaskStatus taskStatus { get; set; }
        public User assignedUser { get; set; }
        public AdvertisingCampaign campaign { get; set; }

        public Task(long id, string title, string body, DateOnly deadline, int statusId, string assignedUserId, long campaignId)
        {
            this.id = id;
            this.title = title;
            this.body = body;
            this.deadline = deadline;
            this.statusId = statusId;
            this.assignedUserId = assignedUserId;
            this.campaignId = campaignId;
        }
    }
}
