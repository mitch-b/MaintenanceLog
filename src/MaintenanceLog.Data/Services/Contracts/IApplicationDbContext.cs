using MaintenanceLog.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog.Data.Services.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<Property>? Properties { get; set; }
        DbSet<Area>? Areas { get; set; }
        DbSet<Asset>? Assets { get; set; }
        
        Task<int> SaveChangesAsync();
    }
}
