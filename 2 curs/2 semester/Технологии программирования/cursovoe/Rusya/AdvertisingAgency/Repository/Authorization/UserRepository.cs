using System.Text;
using AdvertisingAgency.Data;
using AdvertisingAgency.Model.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Repository.Authorization;

public class UserRepository
{
    private readonly AdvertisingDbContext context;

    public UserRepository(AdvertisingDbContext context)
    {
        this.context = context;
    }

    public async Task<User> get(string id)
    {
        return await context.users
            .Where(u => u.Id == id)
            .Include(u => u.role)
            .Include(u => u.tasks)
            .FirstAsync();
    }

    public async Task<bool> isUserExists(string email)
    {
        return await context.users
            .AnyAsync(u => u.Email.Equals(email));
    }

    public async Task<User> getByEmail(String email)
    {
        return await context.users
            .Where(u => u.Email.Equals(email))
            .Include(u=>u.role)
            .FirstAsync();
    }

    public async Task<List<User>> getAll()
    {
        return await context.users
            .Include(u => u.role)
            .Include(u => u.tasks)
            .ToListAsync();
    }

    public async Task<User> save(User user)
    {
        await context.users
            .AddAsync(user);
        await context.SaveChangesAsync();
        return await get(user.Id);
    }

    public async Task<User> update(string id, User user)
    {
        User existedUser = await get(id);
        if (existedUser != null)
        {
            try
            {
                existedUser.roleId = user.roleId;
                existedUser.Email = user.Email;
                existedUser.UserName = user.UserName;
                existedUser.PasswordHash = user.PasswordHash;
                await context.SaveChangesAsync();
                return await get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        throw new Exception($"User with id {id} not found");
    }

    public async Task<bool> delete(string id)
    {
        User existedUser = await get(id);
        if (existedUser != null)
        {
            try
            {
                context.Remove(existedUser);
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