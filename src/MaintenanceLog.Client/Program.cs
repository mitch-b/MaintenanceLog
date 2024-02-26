using Blazor.QrCodeGen;
using MaintenanceLog.Client;
using MaintenanceLog.Data.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddMaintenanceLogClientDataServices(builder.HostEnvironment);

builder.Services.AddTransient(sp => new ModuleCreator(sp.GetService<IJSRuntime>()));

await builder.Build().RunAsync();
