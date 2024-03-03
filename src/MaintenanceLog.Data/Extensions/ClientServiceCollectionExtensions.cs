using MaintenanceLog.Data.Services.Client;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceLog.Data.Extensions
{
    public static class ClientServiceCollectionExtensions
    {
        public static IServiceCollection AddMaintenanceLogClientDataServices(this IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
        {
            services.AddScoped(http => new HttpClient { BaseAddress = new Uri(hostEnvironment.BaseAddress) });
            services.AddScoped<IPropertyService, HttpPropertyService>();
            services.AddScoped<IAreaService, HttpAreaService>();
            services.AddScoped<IAssetService, HttpAssetService>();
            return services;
        }
    }
}
