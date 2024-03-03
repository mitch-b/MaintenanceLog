using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services.Contracts
{
    public interface IAssetService : IEntityBaseService<Asset>
    {
        public Task<List<Asset>> GetByPropertyAsync(int propertyId);
        public Task<List<Asset>> GetByAreaAsync(int areaId);
    }
}
