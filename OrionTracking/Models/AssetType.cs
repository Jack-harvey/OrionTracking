using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class AssetType
    {
        public AssetType()
        {
            Assets = new HashSet<Asset>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
