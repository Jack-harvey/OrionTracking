using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrionTracking.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OrionTracking.Models
{
    public partial class OrionContext : IdentityDbContext<ApplicationUser>
    {
        public OrionContext()
        {
        }

        public OrionContext(DbContextOptions<OrionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; } = null!;
        public virtual DbSet<AssetChangeTrackingNote> AssetChangeTrackingNotes { get; set; } = null!;
        public virtual DbSet<AssetLocation> AssetLocations { get; set; } = null!;
        public virtual DbSet<AssetModel> AssetModels { get; set; } = null!;
        public virtual DbSet<AssetType> AssetTypes { get; set; } = null!;
        public virtual DbSet<Audit> Audits { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<CompanyDivision> CompanyDivisions { get; set; } = null!;
        public virtual DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; } = null!;
        public virtual DbSet<JobTitle> JobTitles { get; set; } = null!;
        public virtual DbSet<ModelManufacturer> ModelManufacturers { get; set; } = null!;
        public virtual DbSet<Office> Offices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=Orion;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Assets_Employees");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Assets_AssetLocations");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Assets_AssetModels");

                entity.HasOne(d => d.ParentAsset)
                    .WithMany(p => p.InverseParentAsset)
                    .HasForeignKey(d => d.ParentAssetId)
                    .HasConstraintName("FK_Assets_ParentAssets");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assets_AssetTypes");
            });

            modelBuilder.Entity<AssetChangeTrackingNote>(entity =>
            {
                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.AssetChangeTrackingNotes)
                    .HasForeignKey(d => d.AspNetUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetChangeTrackingNotes_AspNetUsers");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetChangeTrackingNotes)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetChangeTrackingNotes_Assets");
            });

            modelBuilder.Entity<AssetModel>(entity =>
            {
                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.AssetModels)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManufacturerId_ModelId");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasOne(d => d.AspNetUser)
                    .WithMany()
                    .HasForeignKey(d => d.AspNetUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Audit_AspNetUsers");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.CompanyDivision)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyDivisionId)
                    .HasConstraintName("FK_Employees_CompanyDivisions");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Employees_Companies");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("FK_Employees_JobTitles");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_ManagerId_EmployeesId_self");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("FK_Employees_Offices");
            });

            modelBuilder.Entity<EmployeeDocument>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeDocuments)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDocuments_Employees");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.EmployeeDocuments)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDocuments_DocumentTypes");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offices_Companies");
            });

            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
