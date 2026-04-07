using AdvertisingAgency.Model.Authorization;
using AdvertisingAgency.Repository.Authorization;
using AdvertisingAgency.Service;

namespace AdvertisingAgency.Data
{
    public class DbInitializer
    {
        public static async Task initialize(IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();
            var userRepository = scope.ServiceProvider
                .GetRequiredService<UserRepository>();
            var roleRepository = scope.ServiceProvider
                .GetRequiredService<RoleRepository>();
            var authService = scope.ServiceProvider
                .GetRequiredService<AuthService>();

            var adminConfig = configuration.GetSection("DefaultAdmin");
            string adminEmail = adminConfig["Email"] ?? "admin@admin.com";
            string adminPassword = adminConfig["Password"] ?? "Admin123!";
            string adminRoleName = adminConfig["Role"] ?? "Admin";

            Role adminRole=null;
            if (!await roleRepository.isRoleExists(adminRoleName))
            {
                adminRole = await roleRepository.save(new Role("Admin"));
            }
            else adminRole = await roleRepository.getByName(adminRoleName);

            User adminUser = null;
            if (!await userRepository.isUserExists(adminEmail))
            {
                adminUser = await userRepository
                    .save(new User("Admin",adminEmail,
                    authService.hashPassword(null, adminPassword),
                    adminRole.Id));
            }
        }
    }
}
