using MaintenanceLog.Common.Models.Configuration;
using MaintenanceLog.Common.Services;
using MaintenanceLog.Data.Services.Contracts;
using MaintenanceLog.Data.Services.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceLog.Data.Extensions
{
    public static class ServerServiceCollectionExtensions
    {
        public static IServiceCollection AddMaintenanceLogDataServices(this IServiceCollection services)
        {
            using var localServiceProvider = services.BuildServiceProvider();

            var maintenanceLogSettings = localServiceProvider.GetService<MaintenanceLogSettings>();
            var databaseConfigurationService = localServiceProvider.GetService<IDatabaseConfigurationService>();

            var connectionString = databaseConfigurationService?.GetConnectionString()
                ?? throw new InvalidOperationException("Connection string not found.");

            // check if Settings.DbProvider is MSSQL
            if (string.Equals(maintenanceLogSettings!.Database.DbProvider, "MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString, providerOptions => { 
                        providerOptions.EnableRetryOnFailure(); 
                        providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }));
            }
            else if (string.Equals(maintenanceLogSettings!.Database.DbProvider, "SQLite", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlite(connectionString, providerOptions =>
                    {
                        providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }));
            }

            services.AddScoped(http => new HttpClient
            {
                BaseAddress = new Uri(maintenanceLogSettings.BaseUri)
            });

            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ITaskTypeService, TaskTypeService>();
            services.AddScoped<ITaskDefinitionService, TaskDefinitionService>();

            return services;
        }
    }
}
