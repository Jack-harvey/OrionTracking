using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OrionTracking.Areas.Identity.Data;
using OrionTracking.Models;

namespace OrionTracking.Models
{
    [Table("Audit")]
    public partial class Audit
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string ColumnName { get; set; } = null!;
        public int RowId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
        [StringLength(4000)]
        public string OldValue { get; set; } = null!;
        [StringLength(4000)]
        public string NewValue { get; set; } = null!;
        [StringLength(450)]
        public string AspNetUserId { get; set; } = null!;
        [StringLength(4000)]
        public string? Note { get; set; }

        [ForeignKey("AspNetUserId")]
        [InverseProperty("Audits")]
        public virtual ApplicationUser AspNetUser { get; set; } = null!;
    }
}
