using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class Company
    {
        public Company()
        {
            Employees = new HashSet<Employee>();
            Offices = new HashSet<Office>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [StringLength(255)]
        public string? Domain { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Office> Offices { get; set; }
    }
}
