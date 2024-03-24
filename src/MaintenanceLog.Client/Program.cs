using MaintenanceLog.Client.Extensions;
using MaintenanceLog.Common.Extensions;
using MaintenanceLog.Data.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMaintenanceLogCommonServices(builder.Configuration);
builder.Services.AddMaintenanceLogClientDataServices();
builder.Services.AddMaintenanceLogClientServices(builder);

await builder.Build().RunAsync();
