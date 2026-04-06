namespace AdvertisingAgency.Contract.User;

public class UserCreateContract
{
    public string fullname { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int roleId { get; set; }

    public UserCreateContract(string fullname, string email, string password, int roleId)
    {
        this.fullname = fullname;
        this.email = email;
        this.password = password;
        this.roleId = roleId;
    }
}