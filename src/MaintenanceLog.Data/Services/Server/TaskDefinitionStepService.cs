using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class TaskDefinitionStepService(ILogger<TaskDefinitionStepService> logger, IDbContextFactory<ApplicationDbContext> contextFactory) : ITaskDefinitionStepService
    {
        private readonly ILogger<TaskDefinitionStepService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<TaskDefinitionStep> AddAsync(TaskDefinitionStep entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding taskDefinitionStep: {entity}");
            var result = await context.TaskDefinitionSteps!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting taskDefinitionStep id {id}");
            var result = await context.TaskDefinitionSteps!.FindAsync(id);
            if (result != null)
            {
                var associatedTaskInstanceSteps = await context.TaskInstanceSteps!
                    .Where(tis => tis.TaskDefinitionStepId == id)
                    .ToListAsync();
                foreach (var taskInstanceStep in associatedTaskInstanceSteps)
                {
                    context.TaskInstanceSteps!.Remove(taskInstanceStep);
                }
                await context.SaveChangesAsync();
                context.TaskDefinitionSteps.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"TaskDefinitionStep id {id} not found");
            }
        }

        public async Task<List<TaskDefinitionStep>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitionSteps!.Where(p => !p.Deleted).ToListAsync();
        }

        public async Task<TaskDefinitionStep?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitionSteps!.FindAsync([id]);
        }

        public async Task<TaskDefinitionStep> UpdateAsync(TaskDefinitionStep entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating taskDefinitionStep: {entity}");
            var result = context.TaskDefinitionSteps!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<TaskDefinitionStep>> GetByTaskDefinitionAsync(int taskDefinitionId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskDefinitionSteps!
                .Where(tds => !tds.Deleted)
                .Include(tds => tds.TaskDefinition)
                .Where(tds => tds.TaskDefinition != null && tds.TaskDefinition.Id == taskDefinitionId)
                .ToListAsync();
        }
    }
}
