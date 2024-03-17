using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Extensions;
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
    public DbSet<TaskInstance>? TaskInstances { get; set; }
    public DbSet<TaskDefinitionStep>? TaskDefinitionSteps { get; set; }
    public DbSet<TaskInstanceStep>? TaskInstanceSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        SqlDefaultValueAttributeConvention.Apply(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
