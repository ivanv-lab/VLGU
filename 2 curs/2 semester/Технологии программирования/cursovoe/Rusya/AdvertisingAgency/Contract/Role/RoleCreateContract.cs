namespace AdvertisingAgency.Contract.Role
{
    public class RoleCreateContract
    {
        public string name { get; set; }

        public RoleCreateContract(string name)
        {
            this.name = name; 
        }
    }
}
