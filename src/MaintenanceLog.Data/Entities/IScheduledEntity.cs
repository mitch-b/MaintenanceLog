namespace MaintenanceLog.Data.Entities
{
    public interface IScheduledEntity
    {
        public string? Name { get; }
        public string? CronSchedule { get; set; }
    }
}
