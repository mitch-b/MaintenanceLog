namespace MaintenanceLog.Common.Contracts;
public interface ISmartScheduleService
{
    public Task<string?> EstimateCronScheduleForItem(string? itemName, string[]? overridePrompts = null);
}
