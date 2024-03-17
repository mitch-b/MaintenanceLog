using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class TaskInstanceService(ILogger<TaskInstanceService> logger, IDbContextFactory<ApplicationDbContext> contextFactory) : ITaskInstanceService
    {
        private readonly ILogger<TaskInstanceService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<TaskInstance> AddAsync(TaskInstance entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding Task instance: {entity}");
            var result = await context.TaskInstances!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting Task instance id {id}");
            var result = await context.TaskInstances!.FindAsync(id);
            if (result != null)
            {
                context.TaskInstances.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Task instance id {id} not found");
            }
        }

        public async Task<List<TaskInstance>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstances!
                .Where(td => !td.Deleted)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Asset)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.TaskType)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Area)
                .Include(ti => ti.TaskInstanceSteps)
                .ThenInclude(tis => tis.TaskDefinitionStep)
                .ToListAsync();
        }

        public async Task<TaskInstance?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstances!
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Asset)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.TaskType)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Area)
                .Include(ti => ti.TaskInstanceSteps)
                .ThenInclude(tis => tis.TaskDefinitionStep)
                .FirstOrDefaultAsync(td => td.Id == id);
        }

        public async Task<TaskInstance> UpdateAsync(TaskInstance entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating Task instance: {entity}");
            var result = context.TaskInstances!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<TaskInstance>> GetByPropertyAsync(int propertyId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstances!
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Asset)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.TaskType)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Area)
                .Include(ti => ti.TaskInstanceSteps)
                .ThenInclude(tis => tis.TaskDefinitionStep)
                .Where(ti => !ti.Deleted)
                .Where(ti => ti.TaskDefinition!.Area == null || ti.TaskDefinition.Area.PropertyId == propertyId)
                .Where(ti => ti.TaskDefinition!.Asset == null || ti.TaskDefinition.Asset.Area!.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<List<TaskInstance>> GetByAreaAsync(int areaId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstances!
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Asset)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.TaskType)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Area)
                .Include(ti => ti.TaskInstanceSteps)
                .ThenInclude(tis => tis.TaskDefinitionStep)
                .Where(ti => !ti.Deleted)
                .Where(ti => ti.TaskDefinition != null && ti.TaskDefinition.AreaId == areaId)
                .ToListAsync();
        }

        public async Task<List<TaskInstance>> GetByAssetAsync(int assetId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstances!
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Asset)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.TaskType)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Area)
                .Include(ti => ti.TaskInstanceSteps)
                .ThenInclude(tis => tis.TaskDefinitionStep)
                .Where(ti => !ti.Deleted)
                .Where(ti => ti.TaskDefinition != null && ti.TaskDefinition.AssetId == assetId)
                .ToListAsync();
        }

        public async Task<List<TaskInstance>> GetByTaskTypeAsync(int taskTypeId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstances!
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Asset)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.TaskType)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Area)
                .Include(ti => ti.TaskInstanceSteps)
                .ThenInclude(tis => tis.TaskDefinitionStep)
                .Where(ti => !ti.Deleted)
                .Where(ti => ti.TaskDefinition != null && ti.TaskDefinition.TaskTypeId == taskTypeId)
                .ToListAsync();
        }

        public async Task<List<TaskInstance>> GetByTaskDefinitionAsync(int taskDefinitionId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstances!
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Asset)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.TaskType)
                .Include(ti => ti.TaskDefinition)
                .ThenInclude(td => td.Area)
                .Include(ti => ti.TaskInstanceSteps)
                .ThenInclude(tis => tis.TaskDefinitionStep)
                .Where(ti => !ti.Deleted)
                .Where(ti => ti.TaskDefinition != null && ti.TaskDefinition.Id == taskDefinitionId)
                .ToListAsync();
        }
    }
}
