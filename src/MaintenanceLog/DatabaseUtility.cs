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

        if (!await context.Database.EnsureCreatedAsync())
        {
            Console.WriteLine("Database already exists or was not otherwise created.");
        }
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            // not good with distributed UI... but for now...
            Console.WriteLine("Applying migrations...");
            await context.Database.MigrateAsync();
        }
    }
}
