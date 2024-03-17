using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Migrations;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class TaskInstanceStepService(ILogger<TaskInstanceStepService> logger, IDbContextFactory<ApplicationDbContext> contextFactory, ITaskDefinitionStepService taskDefinitionStepService) : ITaskInstanceStepService
    {
        private readonly ILogger<TaskInstanceStepService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;
        private readonly ITaskDefinitionStepService _taskDefinitionStepService = taskDefinitionStepService;

        public async Task<TaskInstanceStep> AddAsync(TaskInstanceStep entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding taskInstanceStep: {entity}");
            var result = await context.TaskInstanceSteps!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting taskInstanceStep id {id}");
            var result = await context.TaskInstanceSteps!.FindAsync(id);
            if (result != null)
            {
                context.TaskInstanceSteps.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"TaskInstanceStep id {id} not found");
            }
        }

        public async Task<List<TaskInstanceStep>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstanceSteps!.Where(p => !p.Deleted).ToListAsync();
        }

        public async Task<TaskInstanceStep?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstanceSteps!.FindAsync([id]);
        }

        public async Task<TaskInstanceStep> UpdateAsync(TaskInstanceStep entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating taskInstanceStep: {entity}");
            var result = context.TaskInstanceSteps!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<TaskInstanceStep>> GetByTaskInstanceAsync(int taskInstanceId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.TaskInstanceSteps!
                .Where(tis => !tis.Deleted)
                .Include(tis => tis.TaskInstance)
                .Include(tis => tis.TaskDefinitionStep)
                .ThenInclude(tds => tds.TaskDefinition)
                .Where(tds => tds.TaskInstance != null && tds.TaskInstance.Id == taskInstanceId)
                .ToListAsync();
        }

        public async Task<List<TaskInstanceStep>> CreateFromTaskDefinitionAsync(int taskInstanceId, int taskDefinitionId)
        {
            var createdTaskInstanceSteps = new List<TaskInstanceStep>();
            var taskDefinitionSteps = await _taskDefinitionStepService.GetByTaskDefinitionAsync(taskDefinitionId);
            foreach (var taskDefinitionStep in taskDefinitionSteps)
            {
                createdTaskInstanceSteps.Add(await CreateFromTaskDefinitionStepAsync(taskInstanceId, taskDefinitionStep.Id));
            }
            return createdTaskInstanceSteps;
        }

        public async Task<TaskInstanceStep> CreateFromTaskDefinitionStepAsync(int taskInstanceId, int taskDefinitionStepId)
        {
            var taskInstanceStep = new TaskInstanceStep
            {
                TaskDefinitionStepId = taskDefinitionStepId,
                TaskInstanceId = taskInstanceId,
            };
            return await AddAsync(taskInstanceStep);
        }
    }
}
