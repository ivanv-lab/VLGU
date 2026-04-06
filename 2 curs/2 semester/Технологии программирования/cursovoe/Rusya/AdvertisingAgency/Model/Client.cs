namespace AdvertisingAgency.Model
{
    public class Client
    {
        public long id { get; set; }
        public string name { get; set; }
        public string contactPersonFullname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }

        public ICollection<AdvertisingCampaign> campaigns { get; set; }
    }
}
