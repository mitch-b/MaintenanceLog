using System.ComponentModel.DataAnnotations;

namespace MaintenanceLog.Data;

public class Property
{
    public int Id { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Property name cannot exceed 500 characters.")]
    public string? Name { get; set; }

    [StringLength(2000, ErrorMessage = "Property description cannot exceed 2000 characters.")]
    public string? Description { get; set; }
}
