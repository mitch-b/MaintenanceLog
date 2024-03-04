using MaintenanceLog.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    // Magic string.
    public static readonly string MaintenanceLogDb = nameof(MaintenanceLogDb).ToLower();

    public DbSet<Area>? Areas { get; set; }
    public DbSet<Asset>? Assets { get; set; }
    public DbSet<Property>? Properties { get; set; }
    public DbSet<TaskType>? TaskTypes { get; set; }
    public DbSet<TaskDefinition>? TaskDefinitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>();
        modelBuilder.Entity<Asset>();
        modelBuilder.Entity<Property>();
        modelBuilder.Entity<TaskType>();
        modelBuilder.Entity<TaskDefinition>();

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
