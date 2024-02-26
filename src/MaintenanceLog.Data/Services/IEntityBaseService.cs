using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Services
{
    public interface IEntityBaseService<T> where T : BaseEntity
    {
        public Task<List<T>> GetAsync();
        public Task<T?> FindAsync(int id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(int id);
        public Task DeleteAsync(T entity) => DeleteAsync(entity.Id);
    }
}
