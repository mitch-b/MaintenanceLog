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

        if (!await context.Database.EnsureCreatedAsync())
        {
            Console.WriteLine("Database already exists or was not otherwise created.");
        }

        try
        {
            // not good with distributed UI... but for now...
            Console.WriteLine("Applying migrations...");
            var migrations = await context.Database.GetPendingMigrationsAsync();
            foreach (var migration in migrations)
            {
                // Execute all migrations in one single transaction
                using var tran = await context.Database.BeginTransactionAsync();
                try
                {
                    await migrator.MigrateAsync(migration);
                    await tran.CommitAsync();
                }
                catch (Exception exc)
                {
                    await tran.RollbackAsync();
                    throw new Exception($"Error while applying db migration '{migration}'.", exc);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying migrations: {ex.Message}");
        }
    }
}
