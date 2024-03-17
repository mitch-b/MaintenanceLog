using System.ComponentModel.DataAnnotations;

namespace MaintenanceLog.Data.Entities;

public class TaskDefinitionStep : BaseEntity
{
    [StringLength(500, ErrorMessage = "Task Definition Step name cannot exceed 500 characters.")]
    public required string Name { get; set; }

    public bool? IsOptional { get; set; }

    public int? TaskDefinitionId { get; set; }
    public TaskDefinition? TaskDefinition { get; set; }
}
