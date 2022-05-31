using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OrionTracking.Areas.Identity.Data;

namespace OrionTracking.Models
{
    public partial class AssetChangeTrackingNote
    {
        [Key]
        public int Id { get; set; }
        [StringLength(1000)]
        public string Note { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
        [StringLength(450)]
        public string AspNetUserId { get; set; } = null!;
        public int AssetId { get; set; }
        [StringLength(255)]
        public string? OldValue { get; set; }
        [StringLength(255)]
        public string? NewValue { get; set; }

        [ForeignKey("AspNetUserId")]
        [InverseProperty("AssetChangeTrackingNotes")]
        public virtual ICollection<ApplicationUser> AspNetUser { get; set; } = null!;
        [ForeignKey("AssetId")]
        [InverseProperty("AssetChangeTrackingNotes")]
        public virtual Asset Asset { get; set; } = null!;
    }
}
