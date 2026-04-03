using Lab4.Controllers;
using Lab4.Data;
using Lab4.Model.Additional;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Repository.Additional
{
    public class ApplicationStatusRepository
    {
        private readonly DeanDbContext _context;
        private readonly ILog _log = LogManager
            .GetLogger(typeof(ApplicationStatusController));

        public ApplicationStatusRepository(DeanDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationStatus> get(long id)
        {
            _log.Debug($"Get 'ApplicationStatus' by id = '{id}'");
            return await _context.applicationStatuses
                .Where(s => s.id == id)
                .Include(s => s.applications)
                .FirstAsync();
        }

        public async Task<List<ApplicationStatus>> getAll()
        {
            _log.Debug($"Get All 'ApplicationStatus'");
            return await _context.applicationStatuses
                .Include(s => s.applications)
                .ToListAsync();
        }

        public async Task<ApplicationStatus> save(ApplicationStatus applicationStatus)
        {
            _log.Debug($"Save new 'ApplicationStatus':" +
                $"name: {applicationStatus.name}");
            await _context.applicationStatuses
                .AddAsync(applicationStatus);
            await _context.SaveChangesAsync();

            return await get(applicationStatus.id);
        }

        public async Task<ApplicationStatus> update(long id, ApplicationStatus applicationStatus)
        {
            _log.Debug($"Update 'ApplicationStatus':" +
                $"id: {id}," +
                $"new name: {applicationStatus.name}");
            ApplicationStatus existedStatus = await get(id);

            if (existedStatus != null)
            {
                existedStatus.name = applicationStatus.name;
            }

            await _context.SaveChangesAsync();
            return await get(applicationStatus.id);
        }

        public async Task<bool> delete(long id)
        {
            _log.Debug($"Delete 'ApplicationStatus' by id = {id}");
            ApplicationStatus existingStatus = await get(id);

            if (existingStatus != null)
            {
                _context.Remove(existingStatus);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
