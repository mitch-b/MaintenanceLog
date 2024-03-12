using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services.Contracts
{
    public interface ITaskInstanceService : IEntityBaseService<TaskInstance>
    {
        Task<List<TaskInstance>> GetByPropertyAsync(int propertyId);
        Task<List<TaskInstance>> GetByAreaAsync(int areaId);
        Task<List<TaskInstance>> GetByAssetAsync(int assetId);
        Task<List<TaskInstance>> GetByTaskTypeAsync(int taskTypeId);
        Task<List<TaskInstance>> GetByTaskDefinitionAsync(int taskDefinitionId);
    }
}
