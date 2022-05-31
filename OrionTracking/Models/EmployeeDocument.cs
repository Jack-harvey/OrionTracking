using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class EmployeeDocument
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [StringLength(260)]
        public string Path { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime Timestamp { get; set; }
        public int TypeId { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeDocuments")]
        public virtual Employee Employee { get; set; } = null!;
        [ForeignKey("TypeId")]
        [InverseProperty("EmployeeDocuments")]
        public virtual DocumentType Type { get; set; } = null!;
    }
}
