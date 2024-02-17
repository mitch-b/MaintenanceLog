using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    // Magic string.
    public static readonly string RowVersion = nameof(RowVersion);

    // Magic strings.
    public static readonly string MaintenanceLogDb = nameof(MaintenanceLogDb).ToLower();

    public DbSet<Area>? Areas { get; set; }
    public DbSet<Asset>? Assets { get; set; }
    public DbSet<Property>? Properties { get; set; }

    // Define the model.
    // modelBuilder: The ModelBuilder.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This property isn't on the C# class,
        // so we set it up as a "shadow" property and use it for concurrency.
        modelBuilder.Entity<Area>()
            .Property<byte[]>(RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Asset>()
            .Property<byte[]>(RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Property>()
            .Property<byte[]>(RowVersion)
            .IsRowVersion();

        base.OnModelCreating(modelBuilder);
    }

    // Dispose pattern.
    public override void Dispose()
    {
        Debug.WriteLine($"{ContextId} context disposed.");
        base.Dispose();
    }

    // Dispose pattern.
    public override ValueTask DisposeAsync()
    {
        Debug.WriteLine($"{ContextId} context disposed async.");
        return base.DisposeAsync();
    }
}
