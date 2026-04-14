namespace AdvertisingAgency.Contract.Role
{
    public class RoleGetContract
    {
        public string id {  get; set; }
        public string name { get; set; }

        public RoleGetContract(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
