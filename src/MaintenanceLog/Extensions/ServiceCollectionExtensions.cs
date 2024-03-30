using System.Net.Http.Headers;
using MaintenanceLog.Common.Contracts;
using MaintenanceLog.Common.Models.Configuration;
using MaintenanceLog.Data.Entities;
using MaintenanceLog.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MaintenanceLog.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMaintenanceLogServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();

        // Add OpenAIClient to the service collection
        var apiKey = services.BuildServiceProvider().GetRequiredService<IOptions<MaintenanceLogSettings>>().Value?.OpenAI.ApiKey;
        services.AddHttpClient("OpenAI", c =>
        {
            c.BaseAddress = new Uri("https://api.openai.com/v1/");
            c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddScoped<IOpenAIService, OpenAIService>();
        services.AddScoped<ISmartScheduleService, OpenAISmartScheduleService>();
        services.AddScoped<ISmartTaskDefinitionStepService, OpenAISmartTaskDefinitionStepService>();

        return services;
    }
}
