namespace MaintenanceLog.Data.Entities;

public class TaskInstance : BaseEntity
{
    public DateTimeOffset StartedOn { get; set; }
    public DateTimeOffset? CompletedOn { get; set; }
    public DateTimeOffset? DueBy { get; set; }
    public string? Notes { get; set; }

    public string? AutomationMessage { get; set; }

    public int TaskDefinitionId { get; set; }
    public TaskDefinition? TaskDefinition { get; set; }

    public ApplicationUser? CreatedBy { get; set; } = null!;
    public ApplicationUser? AssignedTo { get; set; } = null!;

    public ICollection<TaskInstanceStep>? TaskInstanceSteps { get; set; }
}
