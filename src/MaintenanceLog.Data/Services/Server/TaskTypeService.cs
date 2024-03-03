using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class TaskTypeService(ILogger<TaskTypeService> logger, IDbContextFactory<ApplicationDbContext> contextFactory) : ITaskTypeService
    {
        private readonly ILogger<TaskTypeService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<TaskType> AddAsync(TaskType entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding TaskType: {entity}");
            var result = await context.TaskTypes!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting TaskType id {id}");
            var result = await context.TaskTypes!.FindAsync(id);
            if (result != null)
            {
                context.TaskTypes.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"TaskType id {id} not found");
            }
        }

        public async Task<List<TaskType>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskTypes!.Where(p => !p.Deleted).ToListAsync();
        }

        public async Task<TaskType?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskTypes!.FindAsync([id]);
        }

        public async Task<TaskType> UpdateAsync(TaskType entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating TaskType: {entity}");
            var result = context.TaskTypes!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
