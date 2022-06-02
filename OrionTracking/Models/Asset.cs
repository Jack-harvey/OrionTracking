using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class Asset
    {
        public Asset()
        {
            InverseParentAsset = new HashSet<Asset>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }
        [StringLength(255)]
        public string? CompanyTrackingId { get; set; }
        [StringLength(255)]
        public string? SerialNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PurchaseDate { get; set; }
        [StringLength(255)]
        public string? PurchaseValue { get; set; }
        public bool Active { get; set; }
        public int? ModelId { get; set; }
        public int TypeId { get; set; }
        public int? EmployeeId { get; set; }
        public int? LocationId { get; set; }
        public int? ParentAssetId { get; set; }
        [StringLength(255)]
        public string? MobileNumber { get; set; }
        public bool IsMobileService { get; set; }
        public bool ToBeReturned { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("Assets")]
        public virtual Employee? Employee { get; set; }
        [ForeignKey("LocationId")]
        [InverseProperty("Assets")]
        public virtual AssetLocation? Location { get; set; }
        [ForeignKey("ModelId")]
        [InverseProperty("Assets")]
        public virtual AssetModel? Model { get; set; }
        [ForeignKey("ParentAssetId")]
        [InverseProperty("InverseParentAsset")]
        public virtual Asset? ParentAsset { get; set; }
        [ForeignKey("TypeId")]
        [InverseProperty("Assets")]
        public virtual AssetType Type { get; set; } = null!;
        [InverseProperty("ParentAsset")]
        public virtual ICollection<Asset> InverseParentAsset { get; set; }
    }
}
