using System.ComponentModel.DataAnnotations;

namespace MaintenanceLog.Data.Entities;

public class TaskType : BaseEntity
{
    [StringLength(500, ErrorMessage = "Task type name cannot exceed 500 characters.")]
    public required string Name { get; set; }

    // ICollection<Task> Tasks { get; set; }
}
