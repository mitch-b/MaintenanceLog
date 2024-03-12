using MaintenanceLog.Data.Services.Client;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceLog.Data.Extensions
{
    public static class ClientServiceCollectionExtensions
    {
        public static IServiceCollection AddMaintenanceLogClientDataServices(this IServiceCollection services)
        {

            services.AddScoped<JsonHttpClient>();
            services.AddScoped<IPropertyService, HttpPropertyService>();
            services.AddScoped<IAreaService, HttpAreaService>();
            services.AddScoped<IAssetService, HttpAssetService>();
            services.AddScoped<ITaskTypeService, HttpTaskTypeService>();
            services.AddScoped<ITaskDefinitionService, HttpTaskDefinitionService>();
            services.AddScoped<ITaskInstanceService, HttpTaskInstanceService>();
            return services;
        }
    }
}
