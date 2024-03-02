using MaintenanceLog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services
{
    public class AreaService(ILogger<AreaService> logger, ApplicationDbContext context) : IAreaService
    {
        private readonly ILogger<AreaService> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        public async Task<Area> AddAsync(Area entity)
        {
            _logger.LogDebug($"Adding area: {entity}");
            var result = await _context.Areas!.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting area id {id}");
            var result = await _context.Areas!.FindAsync(id);
            if (result != null)
            {
                _context.Areas.Remove(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Area id {id} not found");
            }
        }

        public async Task<List<Area>> GetAsync()
        {
            return await _context.Areas!.Where(p => !p.Deleted).ToListAsync();
        }

        public async Task<Area?> FindAsync(int id)
        {
            return await _context.Areas!.FindAsync([id]);
        }

        public async Task<Area> UpdateAsync(Area entity)
        {
            _logger.LogDebug($"Updating area: {entity}");
            var result = _context.Areas!.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<Area>> GetByPropertyAsync(int propertyId)
        {
            return await _context.Areas!
                .Where(p => !p.Deleted)
                .Where(p => p.PropertyId == propertyId)
                .ToListAsync();
        }
    }
}
