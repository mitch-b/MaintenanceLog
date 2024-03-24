using Blazor.QrCodeGen;
using Blazored.LocalStorage;
using MaintenanceLog.Client.Services;
using MaintenanceLog.Common.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace MaintenanceLog.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMaintenanceLogClientServices(this IServiceCollection services, WebAssemblyHostBuilder hostBuilder)
    {
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();
        services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
        services.AddBlazoredLocalStorage();

        services.AddTransient(sp => new ModuleCreator(sp.GetService<IJSRuntime>()));
        services.AddScoped(http => new HttpClient { BaseAddress = new Uri(hostBuilder.HostEnvironment.BaseAddress) });

        services.AddScoped<ISmartScheduleService, HttpSmartScheduleService>();

        return services;
    }
}
