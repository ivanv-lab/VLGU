namespace AdvertisingAgency.Contract.Task
{
    public class TaskGetContract
    {
        public long id { get; set; }
        public string title { get; set; }
        public string body {  get; set; }
        public DateOnly deadline { get; set; }
        public string? statusName { get; set; }
        public string? assignedUser { get; set; }
        public string? campaignName { get; set; }

        public TaskGetContract(long id, string title, string body, DateOnly deadline, string statusName, string assignedUser, string campaignName)
        {
            this.id = id;
            this.title = title;
            this.body = body;
            this.deadline = deadline;
            this.statusName = statusName;
            this.assignedUser = assignedUser;
            this.campaignName = campaignName;
        }
    }
}
