using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class TaskDefinitionService(ILogger<TaskDefinitionService> logger, IDbContextFactory<ApplicationDbContext> contextFactory) : ITaskDefinitionService
    {
        private readonly ILogger<TaskDefinitionService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<TaskDefinition> AddAsync(TaskDefinition entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding Task definition: {entity}");
            var result = await context.TaskDefinitions!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting Task definition id {id}");
            var result = await context.TaskDefinitions!.FindAsync(id);
            if (result != null)
            {
                context.TaskDefinitions.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"TaskDefinition id {id} not found");
            }
        }

        public async Task<List<TaskDefinition>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitions!
                .Where(td => !td.Deleted)
                .Include(td => td.Asset)
                .Include(td => td.Area)
                .Include(td => td.TaskType)
                .ToListAsync();
        }

        public async Task<TaskDefinition?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitions!
                .Include(td => td.Asset)
                .Include(td => td.Area)
                .Include(td => td.TaskType)
                .FirstOrDefaultAsync(td => td.Id == id);
        }

        public async Task<TaskDefinition> UpdateAsync(TaskDefinition entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating Task definition: {entity}");
            var result = context.TaskDefinitions!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<TaskDefinition>> GetByPropertyAsync(int propertyId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitions!
                .Include(td => td.Asset)
                .ThenInclude(a => a.Area)
                .ThenInclude(a => a.Property)
                .Include(td => td.TaskType)
                .Where(td => !td.Deleted)
                .Where(td => td.Asset!.Area != null && td.Asset.Area.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<List<TaskDefinition>> GetByAreaAsync(int areaId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitions!
                .Include(td => td.Asset)
                .Include(td => td.Area)
                .Include(td => td.TaskType)
                .Where(td => !td.Deleted)
                .Where(td => td.Asset!.AreaId == areaId)
                .ToListAsync();
        }

        public async Task<List<TaskDefinition>> GetByAssetAsync(int assetId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitions!
                .Include(td => td.Asset)
                .Include(td => td.TaskType)
                .Where(td => !td.Deleted)
                .Where(td => td.AssetId == assetId)
                .ToListAsync();
        }

        public async Task<List<TaskDefinition>> GetByTaskTypeAsync(int taskTypeId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitions!
                .Include(td => td.Asset)
                .Include(td => td.TaskType)
                .Where(p => !p.Deleted)
                .Where(p => p.TaskTypeId == taskTypeId)
                .ToListAsync();
        }
    }
}
