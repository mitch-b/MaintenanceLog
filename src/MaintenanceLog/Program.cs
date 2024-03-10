using System.Text.Json.Serialization;
using MaintenanceLog.Components;
using MaintenanceLog.Components.Account;
using MaintenanceLog.Data;
using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Extensions;
using MaintenanceLog.Extensions;
using MaintenanceLog.Common.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MaintenanceLog.Common.Models.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddMaintenanceLogServices();
builder.Services.AddMaintenanceLogCommonServices(builder.Configuration);
builder.Services.AddMaintenanceLogDataServices();

// get the MaintenanceLogSettings from the Common project using the builder.Services create scope
using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    var maintenanceLogSettingsOptions = serviceProvider.GetService<IOptions<MaintenanceLogSettings>>()
        ?? throw new InvalidOperationException("MaintenanceLogSettings not found.");
    var maintenanceLogSettings = maintenanceLogSettingsOptions.Value;
    
    builder.Services
        .AddFluentEmail(builder.Configuration.GetValue<string>(maintenanceLogSettings.EmailConfig.SmtpFrom))
        .AddSmtpSender(
            maintenanceLogSettings.EmailConfig.SmtpHost, 
            maintenanceLogSettings.EmailConfig.SmtpPort ?? 587,
            maintenanceLogSettings.EmailConfig.SmtpUser,
            maintenanceLogSettings.EmailConfig.SmtpPass);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

// This section sets up and seeds the database. Seeding is NOT normally
// handled this way in production. 
await using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
await MaintenanceLog.DatabaseUtility.EnsureDbCreatedAndSeedWithDefaults(options);

app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MaintenanceLog.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
