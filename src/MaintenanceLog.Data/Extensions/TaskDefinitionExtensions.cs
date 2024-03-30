using Cronos;
using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data.Extensions
{
    public static class TaskDefinitionExtensions
    {
        public static DateTimeOffset? GetNextScheduledDate(this TaskDefinition taskDefinition, DateTimeOffset? lastCompletedDate = null)
        {
            if (taskDefinition is null)
            {
                throw new ArgumentNullException(nameof(taskDefinition));
            }

            CronExpression? cronExpression = null;

            if (CronExpression.TryParse(taskDefinition.CronSchedule, CronFormat.IncludeSeconds, out cronExpression) || CronExpression.TryParse(taskDefinition.CronSchedule, out cronExpression))
            {
                var nextDueDate = cronExpression.GetNextOccurrence(lastCompletedDate ?? DateTimeOffset.UtcNow, TimeZoneInfo.Local);
                return nextDueDate;
            }

            return null;
        }

        public static IEnumerable<DateTimeOffset>? GetNextScheduledDates(this TaskDefinition taskDefinition, DateTimeOffset untilDate, DateTimeOffset? lastCompletedDate = null)
        {
            if (taskDefinition is null)
            {
                throw new ArgumentNullException(nameof(taskDefinition));
            }

            CronExpression? cronExpression = null;

            if (CronExpression.TryParse(taskDefinition.CronSchedule, CronFormat.IncludeSeconds, out cronExpression) || CronExpression.TryParse(taskDefinition.CronSchedule, out cronExpression))
            {
                var nextDueDates = cronExpression.GetOccurrences(lastCompletedDate ?? DateTimeOffset.UtcNow, untilDate, TimeZoneInfo.Local, fromInclusive: true, toInclusive: true);
                return nextDueDates;
            }

            return null;
        }
    }
}
