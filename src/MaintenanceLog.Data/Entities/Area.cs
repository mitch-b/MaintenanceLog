using System.ComponentModel.DataAnnotations;
using MaintenanceLog.Data.Entities;

namespace MaintenanceLog.Data;

public class Area : BaseEntity
{
    [Required]
    [StringLength(500, ErrorMessage = "Area name cannot exceed 500 characters.")]
    public required string Name { get; set; }
}
