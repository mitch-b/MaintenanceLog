using MaintenanceLog.Common.Contracts;

namespace MaintenanceLog.Common.Models.Requests;

public record EstimateCronScheduleRequest
{
    public IScheduledEntity? Item { get; set; }
    public string[]? Prompts { get; set; }
}
    