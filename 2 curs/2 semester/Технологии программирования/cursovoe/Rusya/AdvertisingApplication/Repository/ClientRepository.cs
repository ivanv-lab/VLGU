using AdvertisingApplication.Data;
using AdvertisingApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Repository;

public class ClientRepository
{
    private readonly AdvertisingDbContext context;

    public ClientRepository(AdvertisingDbContext context)
    {
        this.context = context;
    }

    public async Task<Client> get(long id)
    {
        return await context.clients
            .Where(c => c.id == id)
            .Include(c => c.campaigns)
            .FirstAsync();
    }

    public async Task<Client> getByName(string contactName)
    {
        return await context.clients
            .Where(c => c.contactPersonFullname.Equals(contactName))
            .Include(c => c.campaigns)
            .FirstAsync();
    }

    public async Task<List<Client>> getAll()
    {
        return await context.clients
            .ToListAsync();
    }

    public async Task<bool> isClientExists(string name)
    {
        return await context.clients
            .AnyAsync(c => c.name.Equals(name));
    }

    public async Task<Client> save(Client client)
    {
        await context.clients.AddAsync(client);
        await context.SaveChangesAsync();

        return await get(client.id);
    }

    public async Task<Client> update(long id, Client client)
    {
        Client existedClient = await get(id);
        if (existedClient != null)
        {
            try
            {
                existedClient.contactPersonFullname = client.contactPersonFullname;
                existedClient.email = client.email;
                existedClient.address = client.address;
                existedClient.phone = client.phone;
                existedClient.name = client.name;
                await context.SaveChangesAsync();
                return await get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        throw new Exception($"Client with id {id} not found");
    }

    public async Task<bool> delete(long id)
    {
        Client existedClient = await get(id);
        if (existedClient != null)
        {
            try
            {
                context.Remove(existedClient);
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