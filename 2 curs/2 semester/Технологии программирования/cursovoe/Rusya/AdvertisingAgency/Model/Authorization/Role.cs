using Microsoft.AspNetCore.Identity;

namespace AdvertisingAgency.Model.Authorization
{
    public class Role:IdentityRole
    {
        public ICollection<User> users;
        public Role(string name) : base(name)
        {
        }
    }
}
