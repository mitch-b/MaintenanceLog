using MaintenanceLog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services
{
    public class AssetService(ILogger<AssetService> logger, ApplicationDbContext context) : IAssetService
    {
        private readonly ILogger<AssetService> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        public async Task<Asset> AddAsync(Asset entity)
        {
            _logger.LogDebug($"Adding asset: {entity}");
            var result = await _context.Assets!.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting asset id {id}");
            var result = await _context.Assets!.FindAsync(id);
            if (result != null)
            {
                _context.Assets.Remove(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Asset id {id} not found");
            }
        }

        public async Task<List<Asset>> GetAsync()
        {
            return await _context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .Where(p => !p.Deleted)
                .ToListAsync();
        }

        public async Task<Asset?> FindAsync(int id)
        {
            return await _context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            _logger.LogDebug($"Updating asset: {entity}");
            var result = _context.Assets!.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<Asset>> GetByPropertyAsync(int propertyId)
        {
            return await _context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .Where(p => !p.Deleted)
                .Where(p => p.Area != null && p.Area.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<List<Asset>> GetByAreaAsync(int areaId)
        {
            return await _context.Assets!
                .Include(a => a.Area)
                .ThenInclude(a => a.Property)
                .Where(p => !p.Deleted)
                .Where(p => p.AreaId == areaId)
                .ToListAsync();
        }
    }
}
