using Microsoft.AspNetCore.Identity;

namespace AdvertisingApplication.Model.Authorization
{
    public class User: IdentityUser
    {
        public string roleId { get; set; }

        public Role role { get; set; }
        public ICollection<Task>? tasks { get; set; }

        public User(string userName, string email, string passwordHash, string roleId)
            : base(userName)
        {
            this.roleId = roleId;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}