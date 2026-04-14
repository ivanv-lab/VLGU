using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace AdvertisingAgency.Model.Authorization
{
    public class User: IdentityUser
    {
        public string roleId { get; set; }
        public long? clientId {  get; set; }

        public Role role { get; set; }
        public Client? client { get; set; }

        [JsonIgnore]
        public ICollection<Task> tasks { get; set; }

        public User(string userName, string email, string passwordHash, string roleId, long? clientId)
            : base(userName)
        {
            this.clientId = clientId;
            this.roleId = roleId;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}