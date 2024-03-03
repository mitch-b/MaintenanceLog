using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services.Contracts
{
    public interface IAreaService : IEntityBaseService<Area>
    {
        public Task<List<Area>> GetByPropertyAsync(int propertyId);
    }
}
