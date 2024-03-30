namespace MaintenanceLog.Common.Models.Responses;

public record EstimateCronScheduleResponse
{
    public string? CronExpression { get; set; }
}
