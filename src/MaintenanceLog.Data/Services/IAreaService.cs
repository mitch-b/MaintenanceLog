using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services
{
    public interface IAreaService : IEntityBaseService<Area>
    {
        public Task<List<Area>> GetByPropertyAsync(int propertyId);
    }
}
