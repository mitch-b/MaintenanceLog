using System.Threading;
using MaintenanceLog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MaintenanceLog;

public static class DatabaseUtility
{
    public static async Task EnsureDbCreatedAndSeedWithDefaults(DbContextOptions<ApplicationDbContext> options, IMigrator migrator)
    {
        var factory = new LoggerFactory();
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>(options)
            .UseLoggerFactory(factory);

        using var context = new ApplicationDbContext(builder.Options);

        //if (!await context.Database.EnsureCreatedAsync())
        //{
        //    Console.WriteLine("Database already exists or was not otherwise created.");
        //}

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
