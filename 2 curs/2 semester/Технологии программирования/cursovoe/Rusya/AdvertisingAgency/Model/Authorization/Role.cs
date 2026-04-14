using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace AdvertisingAgency.Model.Authorization
{
    public class Role:IdentityRole
    {
        [JsonIgnore]
        public ICollection<User> users;
        public Role(string name) : base(name)
        {
        }
    }
}
