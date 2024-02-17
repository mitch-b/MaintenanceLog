using System.ComponentModel.DataAnnotations;

namespace MaintenanceLog.Data;

public class Area
{
    public int Id { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Area name cannot exceed 500 characters.")]
    public string? Name { get; set; }
}
