using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Assets = new HashSet<Asset>();
            CompanyDivisions = new HashSet<CompanyDivision>();
            EmployeeDocuments = new HashSet<EmployeeDocument>();
            InverseManager = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; } = null!;
        [StringLength(255)]
        public string LastName { get; set; } = null!;
        [StringLength(255)]
        public string UserName { get; set; } = null!;
        public int? ManagerId { get; set; }
        public bool Active { get; set; }
        [StringLength(4000)]
        public string? Notes { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [StringLength(255)]
        public string? Email { get; set; }
        public int? JobTitleId { get; set; }
        public int? CompanyDivisionId { get; set; }
        public int? CompanyId { get; set; }
        public int? OfficeId { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Employees")]
        public virtual Company? Company { get; set; }
        [ForeignKey("CompanyDivisionId")]
        [InverseProperty("Employees")]
        public virtual CompanyDivision? CompanyDivision { get; set; }
        [ForeignKey("JobTitleId")]
        [InverseProperty("Employees")]
        public virtual JobTitle? JobTitle { get; set; }
        [ForeignKey("ManagerId")]
        [InverseProperty("InverseManager")]
        public virtual Employee? Manager { get; set; }
        [ForeignKey("OfficeId")]
        [InverseProperty("Employees")]
        public virtual Office? Office { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Asset> Assets { get; set; }
        [InverseProperty("Manager")]
        public virtual ICollection<CompanyDivision> CompanyDivisions { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; }
        [InverseProperty("Manager")]
        public virtual ICollection<Employee> InverseManager { get; set; }
    }
}
