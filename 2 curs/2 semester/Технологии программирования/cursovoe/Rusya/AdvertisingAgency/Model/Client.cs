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

        public Client(long id, string name, string contactPersonFullname, string email, string phone, string address)
        {
            this.id = id;
            this.name = name;
            this.contactPersonFullname = contactPersonFullname;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }
    }
}
