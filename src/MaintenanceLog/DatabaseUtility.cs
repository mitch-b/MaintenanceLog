using MaintenanceLog.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog;

public static class DatabaseUtility
{
    // Method to see the database. Should not be used in production.
    public static async Task EnsureDbCreatedAndSeedWithDefaults(DbContextOptions<ApplicationDbContext> options)
    {
        // Empty to avoid logging while inserting (otherwise will flood console).
        var factory = new LoggerFactory();
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>(options)
            .UseLoggerFactory(factory);

        using var context = new ApplicationDbContext(builder.Options);

        Console.WriteLine("Ensuring database is created and seeded with defaults.");
        Console.WriteLine($"Database connection string: {context.Database.GetDbConnection().ConnectionString}");

        // Result is true if the database had to be created.
        if (await context.Database.EnsureCreatedAsync())
        {
            // ... 
        }
        await context.Database.MigrateAsync();
    }
}
