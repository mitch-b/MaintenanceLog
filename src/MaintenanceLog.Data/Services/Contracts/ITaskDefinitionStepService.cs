using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services.Contracts
{
    public interface ITaskDefinitionStepService : IEntityBaseService<TaskDefinitionStep>
    {
        public Task<List<TaskDefinitionStep>> GetByTaskDefinitionAsync(int taskDefinitionId);
    }
}
