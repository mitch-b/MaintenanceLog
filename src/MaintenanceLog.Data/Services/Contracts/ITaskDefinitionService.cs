using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services.Contracts
{
    public interface ITaskDefinitionService : IEntityBaseService<TaskDefinition>
    {
        Task<List<TaskDefinition>> GetByPropertyAsync(int propertyId);
        Task<List<TaskDefinition>> GetByAreaAsync(int areaId);
        Task<List<TaskDefinition>> GetByAssetAsync(int assetId);
        Task<List<TaskDefinition>> GetByTaskTypeAsync(int taskTypeId);
    }
}
