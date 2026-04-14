namespace AdvertisingAgency.Contract.User
{
    public class UserGetContract
    {
        public string id {  get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string roleId { get; set; }
        public string roleName {  get; set; }
        public long? clientId {  get; set; }
        public string? clientName { get; set; }

        public UserGetContract(string id, string name, string email, string roleId, string roleName, long? clientId, string? clientName)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.roleId = roleId;
            this.roleName = roleName;
            this.clientId = clientId;
            this.clientName = clientName;
        }
    }
}
