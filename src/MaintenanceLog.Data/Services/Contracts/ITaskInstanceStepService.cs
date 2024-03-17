using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services.Contracts
{
    public interface ITaskInstanceStepService : IEntityBaseService<TaskInstanceStep>
    {
        public Task<List<TaskInstanceStep>> GetByTaskInstanceAsync(int taskInstanceId);
        public Task<List<TaskInstanceStep>> CreateFromTaskDefinitionAsync(int taskInstanceId, int taskDefinitionId);
        public Task<TaskInstanceStep> CreateFromTaskDefinitionStepAsync(int taskInstanceId, int taskDefinitionStepId);
    }
}
