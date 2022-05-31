using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class AssetModel
    {
        public AssetModel()
        {
            Assets = new HashSet<Asset>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        public int ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        [InverseProperty("AssetModels")]
        public virtual ModelManufacturer Manufacturer { get; set; } = null!;
        [InverseProperty("Model")]
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
