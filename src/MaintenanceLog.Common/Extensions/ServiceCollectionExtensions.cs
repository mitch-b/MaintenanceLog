using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MaintenanceLog.Common.Models.Configuration;
using MaintenanceLog.Common.Services;

namespace MaintenanceLog.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMaintenanceLogCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MaintenanceLogSettings>(configuration.GetSection(nameof(MaintenanceLogSettings)));
        services.AddScoped<IDatabaseConfigurationService, DatabaseConfigurationService>();
        return services;
    }
}
