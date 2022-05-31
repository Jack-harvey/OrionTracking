using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class Office
    {
        public Office()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string State { get; set; } = null!;
        [StringLength(255)]
        public string PostCode { get; set; } = null!;
        [StringLength(255)]
        public string City { get; set; } = null!;
        [StringLength(255)]
        public string Address { get; set; } = null!;
        [StringLength(255)]
        public string? PhoneNumber { get; set; }
        [Column("POBoxNumber")]
        [StringLength(255)]
        public string? PoboxNumber { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Offices")]
        public virtual Company Company { get; set; } = null!;
        [InverseProperty("Office")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
