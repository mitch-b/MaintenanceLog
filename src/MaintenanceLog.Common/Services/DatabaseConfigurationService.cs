using MaintenanceLog.Common.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Text;

namespace MaintenanceLog.Common.Services;

public interface IDatabaseConfigurationService
{
    string GetConnectionString();
}

public class DatabaseConfigurationService(IOptions<MaintenanceLogSettings> maintenanceLogSettings) : IDatabaseConfigurationService
{
    private readonly MaintenanceLogSettings _maintenanceLogSettings = maintenanceLogSettings.Value;

    public string GetConnectionString()
    {
        var stringBuilder = new StringBuilder();
        if (string.Equals(_maintenanceLogSettings.Database.DbProvider, "MSSQL", StringComparison.OrdinalIgnoreCase))
        {
            stringBuilder.Append($"Server={_maintenanceLogSettings.Database.Host};");
            stringBuilder.Append($"Database={_maintenanceLogSettings.Database.Name};");
            stringBuilder.Append($"User Id={_maintenanceLogSettings.Database.User};");
            stringBuilder.Append($"Password={_maintenanceLogSettings.Database.Password};");
        }
        else if (string.Equals(_maintenanceLogSettings.Database.DbProvider, "SQLite", StringComparison.OrdinalIgnoreCase))
        {
            stringBuilder.Append($"Data Source={_maintenanceLogSettings.Database.Name};");
        }
        else
        {
            throw new InvalidOperationException("Invalid database provider.");
        }
        return stringBuilder.ToString();
    }
}
