namespace AdvertisingAgency.Contract.User;

public class UserCreateContract
{
    public string fullname { get; set; }
    public string email { get; set; }
    public string password {  get; set; }
    public string roleId { get; set; }
    public long? clientId {  get; set; }

    public UserCreateContract(string fullname, string roleId, long? clientId)
    {
        this.fullname = fullname;
        this.roleId = roleId;
        this.clientId = clientId;
    }
}