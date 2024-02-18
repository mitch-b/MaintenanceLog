using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MaintenanceLog.Client.Pages;
using MaintenanceLog.Components;
using MaintenanceLog.Components.Account;
using MaintenanceLog.Data;
using MaintenanceLog.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

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

var connectionString = builder.Configuration.GetConnectionString("MaintenanceLogDb") ?? throw new InvalidOperationException("Connection string 'MaintenanceLogDb' not found.");
// check if Settings.DbProvider is MSSQL
var dbProvider = builder.Configuration.GetValue<string>("DbProvider");
if (string.Equals(dbProvider, "MSSQL", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else if (string.Equals(dbProvider, "SQLite", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    throw new InvalidOperationException("Invalid database provider.");
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services
    .AddFluentEmail(builder.Configuration.GetValue<string>("EmailConfig:SmtpFrom"))
    .AddSmtpSender(
        builder.Configuration.GetValue<string?>("EmailConfig:SmtpHost"), 
        builder.Configuration.GetValue<int?>("EmailConfig:SmtpPort") ?? 587,
        builder.Configuration.GetValue<string?>("EmailConfig:SmtpUser"),
        builder.Configuration.GetValue<string?>("EmailConfig:SmtpPass"));
        
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();

    // This section sets up and seeds the database. Seeding is NOT normally
    // handled this way in production. The following approach is used in this
    // sample app to make the sample simpler. The app can be cloned. The
    // connection string is configured. The app can be run.
    await using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
    var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
    await MaintenanceLog.DatabaseUtility.EnsureDbCreatedAndSeedWithDefaults(options);
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MaintenanceLog.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
