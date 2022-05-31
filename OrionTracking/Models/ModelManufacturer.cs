using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class ModelManufacturer
    {
        public ModelManufacturer()
        {
            AssetModels = new HashSet<AssetModel>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [InverseProperty("Manufacturer")]
        public virtual ICollection<AssetModel> AssetModels { get; set; }
    }
}
