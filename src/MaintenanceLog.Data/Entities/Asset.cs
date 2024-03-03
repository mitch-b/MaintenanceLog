using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MaintenanceLog.Data.Entities;

public class Asset : BaseEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? SerialNumber { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public string? Location { get; set; }

    public string? ImageUrl { get; set; }

    public string? QrCode { get; set; }

    public string? Notes { get; set; }

    public bool IsArchived { get; set; }

    public int? AreaId { get; set; }
    public Area? Area { get; set; }
}
