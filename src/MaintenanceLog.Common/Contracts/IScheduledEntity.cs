namespace MaintenanceLog.Common.Contracts;
public interface IScheduledEntity
{
    public string? Name { get; }
    public string? CronSchedule { get; set; }
}
