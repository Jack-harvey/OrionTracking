using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class AssetLocation
    {
        public AssetLocation()
        {
            Assets = new HashSet<Asset>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [StringLength(255)]
        public string? State { get; set; }
        [StringLength(255)]
        public string? PostCode { get; set; }
        [StringLength(255)]
        public string? City { get; set; }
        [StringLength(255)]
        public string? Street { get; set; }
        [StringLength(4000)]
        public string? Notes { get; set; }
        public bool? Active { get; set; }

        [InverseProperty("Location")]
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
