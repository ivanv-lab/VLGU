using Microsoft.AspNetCore.Identity;

namespace AdvertisingAgency.Model.Authorization
{
    public class Role:IdentityRole
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<User> users { get; set; }
        
        public Role(int id, string name) {
            this.id = id;
            this.name = name;
        }
    }
}
