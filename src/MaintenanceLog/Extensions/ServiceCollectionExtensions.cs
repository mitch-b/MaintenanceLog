using MaintenanceLog.Data.Entities;
using MaintenanceLog.Services;
using Microsoft.AspNetCore.Identity;

namespace MaintenanceLog.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMaintenanceLogServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();
        return services;
    }
}
