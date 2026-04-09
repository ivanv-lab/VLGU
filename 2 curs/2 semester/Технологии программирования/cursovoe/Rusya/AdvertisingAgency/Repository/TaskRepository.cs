using AdvertisingAgency.Data;
using Microsoft.EntityFrameworkCore;
using Task = AdvertisingAgency.Model.Task;

namespace AdvertisingAgency.Repository;

public class TaskRepository
{
    private readonly AdvertisingDbContext context;

    public TaskRepository(AdvertisingDbContext context)
    {
        this.context = context;
    }

    public async Task<Task> get(long id)
    {
        return await context.tasks
            .Where(t => t.id == id)
            .FirstAsync();
    }

    public async Task<List<Task>> getAll()
    {
        return await context.tasks
            .ToListAsync();
    }

    public async Task<Task> save(Task task)
    {
        await context.tasks.AddAsync(task);
        await context.SaveChangesAsync();

        return await get(task.id);
    }

    public async Task<Task> update(long id, Task task)
    {
        Task existedTask = await get(id);
        if (existedTask != null)
        {
            try
            {
                existedTask.title = task.title;
                existedTask.body = task.body;
                existedTask.deadline = task.deadline;
                existedTask.statusId = task.statusId;
                existedTask.assignedUserId = task.assignedUserId;
                existedTask.campaignId = task.campaignId;
                await context.SaveChangesAsync();
                return await get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        throw new Exception($"Task with id {id} not found");
    }

    public async Task<bool> delete(long id)
    {
        Task existedTask = await get(id);
        if (existedTask != null)
        {
            try
            {
                context.Remove(existedTask);
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