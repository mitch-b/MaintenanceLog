namespace MaintenanceLog.Common.Contracts;
public interface ISmartScheduleService
{
    public Task<string?> EstimateCronScheduleForItem(IScheduledEntity scheduledEntity, string[]? prompts = null);
}
