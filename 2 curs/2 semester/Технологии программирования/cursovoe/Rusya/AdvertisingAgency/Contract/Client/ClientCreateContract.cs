namespace AdvertisingAgency.Contract.Client;

public class ClientCreateContract
{
    public string name { get; set; }
    public string contactPersonFullname { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string address { get; set; }

    public ClientCreateContract(string name, string contactPersonFullname, string email, string phone, string address)
    {
        this.name = name;
        this.contactPersonFullname = contactPersonFullname;
        this.email = email;
        this.phone = phone;
        this.address = address;
    }
}