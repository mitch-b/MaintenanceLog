using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services.Server
{
    public class AssetService(ILogger<AssetService> logger, IDbContextFactory<ApplicationDbContext> contextFactory) : IAssetService
    {
        private readonly ILogger<AssetService> _logger = logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<Asset> AddAsync(Asset entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Adding asset: {entity}");
            var result = await context.Assets!.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogInformation($"Deleting asset id {id}");
            var result = await context.Assets!.FindAsync(id);
            if (result != null)
            {
                context.Assets.Remove(result);
                await context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Asset id {id} not found");
            }
        }

        public async Task<List<Asset>> GetAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .Where(p => !p.Deleted)
                .ToListAsync();
        }

        public async Task<Asset?> FindAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            using var context = _contextFactory.CreateDbContext();
            _logger.LogDebug($"Updating asset: {entity}");
            var result = context.Assets!.Update(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<Asset>> GetByPropertyAsync(int propertyId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .Where(p => !p.Deleted)
                .Where(p => p.Area != null && p.Area.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<List<Asset>> GetByAreaAsync(int areaId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .Where(p => !p.Deleted)
                .Where(p => p.AreaId == areaId)
                .ToListAsync();
        }
    }
}
