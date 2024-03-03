using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class AreaService(ILogger<AreaService> logger, IDbContextFactory<ApplicationDbContext> contextFactory) : IAreaService
    {
        private readonly ILogger<AreaService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Area> AddAsync(Area entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding area: {entity}");
            var result = await context.Areas!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting area id {id}");
            var result = await context.Areas!.FindAsync(id);
            if (result != null)
            {
                context.Areas.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Area id {id} not found");
            }
        }

        public async Task<List<Area>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Areas!.Where(p => !p.Deleted).ToListAsync();
        }

        public async Task<Area?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Areas!.FindAsync([id]);
        }

        public async Task<Area> UpdateAsync(Area entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating area: {entity}");
            var result = context.Areas!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<Area>> GetByPropertyAsync(int propertyId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Areas!
                .Where(p => !p.Deleted)
                .Where(p => p.PropertyId == propertyId)
                .ToListAsync();
        }
    }
}
