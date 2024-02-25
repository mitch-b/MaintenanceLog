using System.ComponentModel.DataAnnotations;

namespace MaintenanceLog.Data.Entities;

public class Area : BaseEntity
{
    [Required]
    [StringLength(500, ErrorMessage = "Area name cannot exceed 500 characters.")]
    public required string Name { get; set; }
}
