using MaintenanceLog.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog;

public static class DatabaseUtility
{
    public static async Task EnsureDbCreatedAndSeedWithDefaults(DbContextOptions<ApplicationDbContext> options)
    {
        var factory = new LoggerFactory();
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>(options)
            .UseLoggerFactory(factory);

        using var context = new ApplicationDbContext(builder.Options);

        try
        {
            // not good with distributed UI... but for now...
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying migrations: {ex.Message}");
        }
    }
}
