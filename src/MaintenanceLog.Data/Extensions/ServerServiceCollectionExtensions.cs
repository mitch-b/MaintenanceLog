using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaintenanceLog.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceLog.Data.Extensions
{
    public static class ServerServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var connectionString = configuration?.GetConnectionString("MaintenanceLogDb") 
                ?? throw new InvalidOperationException("Connection string 'MaintenanceLogDb' not found.");

            // check if Settings.DbProvider is MSSQL
            var dbProvider = configuration?.GetSection("DbProvider").Value;
            if (string.Equals(dbProvider, "MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }
            else if (string.Equals(dbProvider, "SQLite", StringComparison.OrdinalIgnoreCase))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(connectionString));
            }
            else
            {
                throw new InvalidOperationException("Invalid database provider.");
            }

            services.AddScoped<IPropertyService, PropertyService>();

            return services;
        }
    }
}
