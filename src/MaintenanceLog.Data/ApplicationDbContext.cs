using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    // Magic string.
    public static readonly string MaintenanceLogDb = nameof(MaintenanceLogDb).ToLower();

    public DbSet<Area>? Areas { get; set; }
    public DbSet<Asset>? Assets { get; set; }
    public DbSet<Property>? Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>();
        modelBuilder.Entity<Asset>();
        modelBuilder.Entity<Property>();

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
