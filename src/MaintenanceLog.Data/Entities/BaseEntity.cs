﻿using System.ComponentModel.DataAnnotations;
using MaintenanceLog.Data.Attributes;

namespace MaintenanceLog.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool Deleted { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        [SqlDefaultValue("GETUTCDATE()")]
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    }
}
