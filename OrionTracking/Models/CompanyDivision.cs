using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class CompanyDivision
    {
        public CompanyDivision()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string? Name { get; set; }
        public int CompanyId { get; set; }
        public int ManagerId { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("CompanyDivisions")]
        public virtual Company Company { get; set; } = null!;
        [InverseProperty("CompanyDivision")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
