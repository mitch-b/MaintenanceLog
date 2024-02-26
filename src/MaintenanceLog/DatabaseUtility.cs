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
        // Result is true if the database had to be created.
        if (await context.Database.EnsureCreatedAsync())
        {
            // ... seed the database
        }
    }
}
