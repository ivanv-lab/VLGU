using AdvertisingAgency.Data;
using AdvertisingAgency.Model;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Repository;

public class CampaignStatusRepository
{
    private readonly AdvertisingDbContext context;

    public CampaignStatusRepository(AdvertisingDbContext context)
    {
        this.context = context;
    }

    public async Task<CampaignStatus> get(int id)
    {
        return await context.campaignStatuses
            .Where(cs => cs.id == id)
            .FirstAsync();
    }

    public async Task<CampaignStatus> getByName(string name)
    {
        return await context.campaignStatuses
            .Where(cs => cs.name.Equals(name))
            .FirstAsync();
    }

    public async Task<bool> isStatusExists(string name)
    {
        return await context.campaignStatuses
            .AnyAsync(cs => cs.name.Equals(name));
    }

    public async Task<List<CampaignStatus>> getAll()
    {
        return await context.campaignStatuses
            .ToListAsync();
    }

    public async Task<CampaignStatus> save(CampaignStatus campaignStatus)
    {
        await context.campaignStatuses.AddAsync(campaignStatus);
        await context.SaveChangesAsync();

        return await get(campaignStatus.id);
    }

    public async Task<CampaignStatus> updated(int id, CampaignStatus campaignStatus)
    {
        CampaignStatus existedStatus = await get(id);
        if (existedStatus != null)
        {
            try
            {
                existedStatus.name = campaignStatus.name;
                await context.SaveChangesAsync();
                return await get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        throw new Exception($"Campaign status with id {id} not found");
    }

    public async Task<bool> delete(int id)
    {
        CampaignStatus existedStatus = await get(id);
        if (existedStatus != null)
        {
            try
            {
                context.Remove(existedStatus);
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