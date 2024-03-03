using MaintenanceLog.Data.Services.Contracts;
using MaintenanceLog.Data.Services.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceLog.Data.Extensions
{
    public static class ServerServiceCollectionExtensions
    {
        public static IServiceCollection AddMaintenanceLogDataServices(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration?.GetConnectionString("MaintenanceLogDb")
                ?? throw new InvalidOperationException("Connection string 'MaintenanceLogDb' not found.");

            // check if Settings.DbProvider is MSSQL
            var dbProvider = configuration?.GetSection("DbProvider").Value;
            if (string.Equals(dbProvider, "MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString, providerOptions => { 
                        providerOptions.EnableRetryOnFailure(); 
                        providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }));
            }
            else if (string.Equals(dbProvider, "SQLite", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlite(connectionString, providerOptions =>
                    {
                        providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }));
            }
            else
            {
                throw new InvalidOperationException("Invalid database provider.");
            }

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

            services.AddScoped(http => new HttpClient
            {
                BaseAddress = new Uri(configuration!.GetSection("BaseUri")!.Value!)
            });

            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IAssetService, AssetService>();

            return services;
        }
    }
}
