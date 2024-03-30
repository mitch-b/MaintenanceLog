namespace MaintenanceLog.Common.Models.Requests;

public record EstimateCronScheduleRequest
{
    public string? ItemName { get; set; }
    public string[]? OverrideSystemPrompts { get; set; }
}
