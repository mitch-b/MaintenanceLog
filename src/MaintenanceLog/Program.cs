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
using Blazored.LocalStorage;

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

// add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MaintenanceLog", Version = "v1" });
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

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMaintenanceLogCommonServices(builder.Configuration);
builder.Services.AddMaintenanceLogDataServices();
builder.Services.AddMaintenanceLogServices();

// get the MaintenanceLogSettings from the Common project using the builder.Services create scope
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    var maintenanceLogSettingsOptions = serviceProvider.GetService<IOptions<MaintenanceLogSettings>>()
        ?? throw new InvalidOperationException("MaintenanceLogSettings not found.");
    var maintenanceLogSettings = maintenanceLogSettingsOptions.Value;
    
    var fromAddress = string.IsNullOrWhiteSpace(maintenanceLogSettings.EmailConfig?.SmtpFrom) 
        ? null 
        : maintenanceLogSettings.EmailConfig.SmtpFrom;
    builder.Services
        .AddFluentEmail(fromAddress)
        .AddSmtpSender(
            maintenanceLogSettings.EmailConfig?.SmtpHost, 
            maintenanceLogSettings.EmailConfig?.SmtpPort ?? 587,
            maintenanceLogSettings.EmailConfig?.SmtpUser,
            maintenanceLogSettings.EmailConfig?.SmtpPass);
}
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MaintenanceLog v1");
    });
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
var options = app.Services.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
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
