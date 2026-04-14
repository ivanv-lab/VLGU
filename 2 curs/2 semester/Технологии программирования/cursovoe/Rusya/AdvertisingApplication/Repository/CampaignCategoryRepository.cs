using AdvertisingApplication.Data;
using AdvertisingApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Repository;

public class CampaignCategoryRepository
{
    private readonly AdvertisingDbContext context;

    public CampaignCategoryRepository(AdvertisingDbContext context)
    {
        this.context = context;
    }

    public async Task<CampaignCategory> get(int id)
    {
        return await context.campaignCategories
            .Where(cc => cc.id == id)
            .FirstAsync();
    }

    public async Task<CampaignCategory> getByName(string name)
    {
        return await context.campaignCategories
            .Where(cc => cc.name.Equals(name))
            .FirstAsync();
    }

    public async Task<bool> isCategoryExists(string name)
    {
        return await context.campaignCategories
            .AnyAsync(cc => cc.name.Equals(name));
    }

    public async Task<List<CampaignCategory>> getAll()
    {
        return await context.campaignCategories
            .ToListAsync();
    }

    public async Task<CampaignCategory> save(CampaignCategory campaignCategory)
    {
        await context.campaignCategories.AddAsync(campaignCategory);
        await context.SaveChangesAsync();

        return await get(campaignCategory.id);
    }

    public async Task<CampaignCategory> update(int id, CampaignCategory campaignCategory)
    {
        CampaignCategory existedCategory = await get(id);
        if (existedCategory != null)
        {
            try
            {
                existedCategory.name = campaignCategory.name;
                await context.SaveChangesAsync();
                return await get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        throw new Exception($"Campaign category with id {id} not found");
    }

    public async Task<bool> delete(int id)
    {
        try
        {
            CampaignCategory existedCategory = await get(id);
            if (existedCategory != null)
            {
                try
                {
                    context.Remove(existedCategory);
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
        catch { return false; }
    }
}