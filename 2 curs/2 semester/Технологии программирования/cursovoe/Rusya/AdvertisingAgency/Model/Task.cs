using AdvertisingAgency.Model.Authorization;

namespace AdvertisingAgency.Model
{
    public class Task
    {
        public long id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateOnly deadline { get; set; }

        public int statusId { get; set; }
        public long assignedUserId { get; set; }
        public long campaignId { get; set; }

        public TaskStatus taskStatus { get; set; }
        public User assignedUser { get; set; }
        public AdvertisingCampaign campaign { get; set; }
    }
}
