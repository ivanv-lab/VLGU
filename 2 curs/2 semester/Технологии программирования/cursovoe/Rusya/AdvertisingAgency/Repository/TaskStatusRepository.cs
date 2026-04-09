using AdvertisingAgency.Data;
using Microsoft.EntityFrameworkCore;
using TaskStatus = AdvertisingAgency.Model.TaskStatus;

namespace AdvertisingAgency.Repository
{
    public class TaskStatusRepository
    {
        private readonly AdvertisingDbContext context;

        public TaskStatusRepository(AdvertisingDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskStatus> get(int id)
        {
            return await context.taskStatuses
                .Where(ts => ts.id == id)
                .FirstAsync();
        }

        public async Task<TaskStatus> getByName(string name)
        {
            return await context.taskStatuses
                .Where(ts => ts.name.Equals(name))
                .FirstAsync();
        }

        public async Task<bool> isTaskStatusExists(string name)
        {
            return await context.taskStatuses
                .AnyAsync(ts => ts.name.Equals(name));
        }

        public async Task<List<TaskStatus>> getAll()
        {
            return await context.taskStatuses
                .ToListAsync();
        }

        public async Task<TaskStatus> save(TaskStatus status)
        {
            await context.taskStatuses.AddAsync(status);
            await context.SaveChangesAsync();

            return await get(status.id);
        }

        public async Task<TaskStatus> update(int id, TaskStatus status)
        {
            TaskStatus existedStatus = await get(id);
            if (existedStatus != null)
            {
                try
                {
                    existedStatus.name = status.name;
                    await context.SaveChangesAsync();
                    return await get(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            throw new Exception($"Task status with id {id} not found");
        }

        public async Task<bool> delete(int id)
        {
            TaskStatus existedStatus = await get(id);
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
}
