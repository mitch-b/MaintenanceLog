namespace MaintenanceLog.Data.Entities;

public class TaskInstanceStep : BaseEntity
{
    public DateTimeOffset? CompletedOn { get; set; }
    public ApplicationUser? CompletedBy { get; set; }

    public int TaskInstanceId { get; set; }
    public TaskInstance? TaskInstance { get; set; }

    public int TaskDefinitionStepId { get; set; }
    public TaskDefinitionStep? TaskDefinitionStep { get; set; }
}
