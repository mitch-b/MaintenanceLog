using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services
{
    public interface IEntityBaseService<T> where T : BaseEntity
    {
        public Task<List<T>> GetPropertiesAsync();
        public Task<T?> GetPropertyAsync(int id);
        public Task<T> AddPropertyAsync(T entity);
        public Task<T> UpdatePropertyAsync(T entity);
        public Task DeletePropertyAsync(int id);
        public Task DeleteAsync(T entity) => DeletePropertyAsync(entity.Id);
    }
}
