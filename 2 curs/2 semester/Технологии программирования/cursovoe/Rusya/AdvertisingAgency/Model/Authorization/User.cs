using Microsoft.AspNetCore.Identity;

namespace AdvertisingAgency.Model.Authorization
{
    public class User: IdentityUser
    {
        public long id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public int roleId { get; set; }

        public Role role { get; set; }
        public ICollection<Task> tasks { get; set; }
        
        public User(long id, string fullname, string email, string passwordHash, int roleId)
        {
            this.id = id;
            this.fullname = fullname;
            this.email = email;
            this.passwordHash = passwordHash;
            this.roleId = roleId;
        }
    }
}