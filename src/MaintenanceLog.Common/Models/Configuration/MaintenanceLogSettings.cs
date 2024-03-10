namespace MaintenanceLog.Common.Models.Configuration
{
    public record MaintenanceLogSettings
    {
        public DatabaseSettings Database { get; set; } = null!;
        public EmailConfigSettings EmailConfig { get; set; } = null!;
        public string BaseUri { get; set; } = null!;
    }
}
