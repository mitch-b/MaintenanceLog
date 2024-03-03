using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class PropertyService(ILogger<PropertyService> logger, IDbContextFactory<ApplicationDbContext> contextFactory) : IPropertyService
    {
        private readonly ILogger<PropertyService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Property> AddAsync(Property entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding property: {entity}");
            var result = await context.Properties!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting property id {id}");
            var result = await context.Properties!.FindAsync(id);
            if (result != null)
            {
                context.Properties.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Property id {id} not found");
            }
        }

        public async Task<List<Property>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Properties!
                .Include(p => p.Areas)
                .Where(p => !p.Deleted)
                .ToListAsync();
        }

        public async Task<Property?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Properties!
                .Include(p => p.Areas)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Property> UpdateAsync(Property entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating property: {entity}");
            var result = context.Properties!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
