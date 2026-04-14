namespace AdvertisingApplication.Contract.User;

public class UserCreateContract
{
    public string fullname { get; set; }
    public string email { get; set; }
    public string password {  get; set; }
    public string roleId { get; set; }

    public UserCreateContract(string fullname, string roleId)
    {
        this.fullname = fullname;
        this.roleId = roleId;
    }
}