using AdvertisingApplication.Data;
using AdvertisingApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Repository;

public class AdvertisingCampaignRepository
{
    private readonly AdvertisingDbContext context;

    public AdvertisingCampaignRepository(AdvertisingDbContext context)
    {
        this.context = context;
    }

    public async Task<AdvertisingCampaign> get(long id)
    {
        return await context.advertisingCampaigns
            .Where(ac => ac.id == id)
            .Include(ac => ac.client)
            .Include(ac => ac.category)
            .Include(ac => ac.status)
            .Include(ac => ac.tasks)
            .FirstAsync();
    }

    public async Task<AdvertisingCampaign> getByName(string name)
    {
        return await context.advertisingCampaigns
            .Where(ac => ac.name.Equals(name))
            .FirstAsync();
    }

    public async Task<bool> isAdvertisingCampaignExists(string name)
    {
        return await context.advertisingCampaigns
            .AnyAsync(ac => ac.name.Equals(name));
    }

    public async Task<List<AdvertisingCampaign>> getAll()
    {
        return await context.advertisingCampaigns
            .ToListAsync();
    }

    public async Task<AdvertisingCampaign> save(AdvertisingCampaign campaign)
    {
        await context.advertisingCampaigns.AddAsync(campaign);
        await context.SaveChangesAsync();

        return await get(campaign.id);
    }

    public async Task<AdvertisingCampaign> update(long id, AdvertisingCampaign campaign)
    {
        AdvertisingCampaign existedCampaign = await get(id);
        if (existedCampaign != null)
        {
            try
            {
                existedCampaign.name = campaign.name;
                existedCampaign.description = campaign.description;
                existedCampaign.startDate = campaign.startDate;
                existedCampaign.endDate = campaign.endDate;
                existedCampaign.budget = campaign.budget;
                existedCampaign.statusId = campaign.statusId;
                existedCampaign.categoryId = campaign.categoryId;
                existedCampaign.clientId = campaign.clientId;
                await context.SaveChangesAsync();
                return await get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        throw new Exception($"Advertising campaign with id {id} not found");
    }

    public async Task<bool> delete(long id)
    {
        AdvertisingCampaign existedCampaign = await get(id);
        if (existedCampaign != null)
        {
            try
            {
                context.Remove(existedCampaign);
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