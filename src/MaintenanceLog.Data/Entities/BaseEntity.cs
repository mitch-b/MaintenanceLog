using System.ComponentModel.DataAnnotations;

namespace MaintenanceLog.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Deleted { get; set; }
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
