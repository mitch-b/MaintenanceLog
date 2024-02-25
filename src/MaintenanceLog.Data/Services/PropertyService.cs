﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaintenanceLog.Data.Services
{
    public class PropertyService(ILogger<PropertyService> logger, ApplicationDbContext context) : IPropertyService
    {
        private readonly ILogger<PropertyService> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        public async Task<Property> AddPropertyAsync(Property entity)
        {
            _logger.LogDebug($"Adding property: {entity}");
            var result = await _context.Properties!.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeletePropertyAsync(int id)
        {
            _logger.LogInformation($"Deleting property id {id}");
            var result = await _context.Properties!.FindAsync(id);
            if (result != null)
            {
                _context.Properties.Remove(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Property id {id} not found");
            }
        }

        public async Task<List<Property>> GetPropertiesAsync()
        {
            return await _context.Properties!.Where(p => !p.Deleted).ToListAsync();
        }

        public async Task<Property?> GetPropertyAsync(int id)
        {
            return await _context.Properties!.FindAsync([id]);
        }

        public async Task<Property> UpdatePropertyAsync(Property entity)
        {
            _logger.LogDebug($"Updating property: {entity}");
            var result = _context.Properties!.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}