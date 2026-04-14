namespace AdvertisingAgency.Contract.Client
{
    public class ClientGetContract
    {
        public long id { get; set; }
        public string name { get; set; }
        public string contactPersonFullname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public List<string> campaignNames { get; set; }

        public ClientGetContract(long id, string name, 
            string contactPersonFullname, string email, 
            string phone, string address,  List<string> campaignNames)
        {
            this.id = id;
            this.name = name;
            this.contactPersonFullname = contactPersonFullname;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.campaignNames = campaignNames;
        }
    }
}
