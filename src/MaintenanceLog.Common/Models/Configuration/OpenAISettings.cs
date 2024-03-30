namespace MaintenanceLog.Common.Models.Configuration
{
    public record OpenAISettings
    {
        public string? ApiKey { get; set; } = null!;
    }
}
