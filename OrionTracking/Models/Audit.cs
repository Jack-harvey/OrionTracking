using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OrionTracking.Areas.Identity.Data;

namespace OrionTracking.Models
{
    [Keyless]
    [Table("Audit")]
    public partial class Audit
    {
        public long? Id { get; set; }
        [StringLength(255)]
        public string ColumnName { get; set; } = null!;
        [StringLength(255)]
        public string RowId { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
        [StringLength(4000)]
        public string OldValue { get; set; } = null!;
        [StringLength(4000)]
        public string NewValue { get; set; } = null!;
        [StringLength(450)]
        public string AspNetUserId { get; set; } = null!;

        [ForeignKey("AspNetUserId")]
        public virtual ICollection<ApplicationUser> AspNetUser { get; set; } = null!;
    }
}
