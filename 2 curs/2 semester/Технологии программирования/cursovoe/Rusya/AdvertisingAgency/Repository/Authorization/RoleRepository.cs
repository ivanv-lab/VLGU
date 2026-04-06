using AdvertisingAgency.Data;
using AdvertisingAgency.Model.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Repository.Authorization
{
    public class RoleRepository
    {
        private readonly AdvertisingDbContext context;

        public RoleRepository(AdvertisingDbContext context)
        {
            this.context = context;
        }

        public async Task<Role> get(int id)
        {
            return await context.roles
                .Where(r => r.id == id)
                .Include(r => r.users)
                .FirstAsync();
        }

        public async Task<bool> isRoleExists(string name)
        {
            return await context.roles
                .AnyAsync(r => r.name.Equals(name));
        }
        
        public async Task<List<Role>> getAll()
        {
            return await context.roles
                .Include(r => r.users)
                .ToListAsync();
        }

        public async Task<Role> save(Role role)
        {
            await context.roles.AddAsync(role);
            await context.SaveChangesAsync();

            return await get(role.id);
        }

        public async Task<Role> update(int id, Role role)
        {
            Role existedRole = await get(id);
            if (existedRole != null)
            {
                try
                {
                    existedRole.name = role.name;
                    await context.SaveChangesAsync();
                    return await get(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception($"Role with id {id} not found");
        }

        public async Task<bool> delete(int id)
        {
            Role existedRole = await get(id);
            if (existedRole != null)
            {
                try
                {
                    context.Remove(existedRole);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }
    }
}
