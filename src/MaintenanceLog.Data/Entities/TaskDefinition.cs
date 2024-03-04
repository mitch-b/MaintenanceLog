using System.ComponentModel.DataAnnotations;

namespace MaintenanceLog.Data.Entities;

public class TaskDefinition : BaseEntity
{
    [StringLength(500, ErrorMessage = "Task definition name cannot exceed 500 characters.")]
    public required string Name { get; set; }

    public int AssetId { get; set; }
    public Asset? Asset { get; set; }
}
