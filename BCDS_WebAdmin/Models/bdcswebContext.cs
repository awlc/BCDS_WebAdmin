using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BCDS_WebAdmin.Models
{
    public partial class bdcswebContext : DbContext
    {
        public bdcswebContext()
        {
        }

        public bdcswebContext(DbContextOptions<bdcswebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<ComponentItems> ComponentItems { get; set; }
        public virtual DbSet<Components> Components { get; set; }
        public virtual DbSet<ComponentTypes> ComponentTypes { get; set; }
        public virtual DbSet<ContractManufacturers> ContractManufacturers { get; set; }
        public virtual DbSet<Contractors> Contractors { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // defined in ConfigureServices in Startup
                //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("bcdswebDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_User_Id");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("User_Id")
                    .HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.RoleId).HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<ComponentItems>(entity =>
            {
                entity.HasKey(e => e.ComponentItemId);

                entity.HasIndex(e => e.ComponentId)
                    .HasName("IX_ComponentId");

                entity.HasIndex(e => e.TagId)
                    .HasName("IX_TagId");

                entity.Property(e => e.DeliveryTimestamp).HasColumnType("datetime");

                entity.Property(e => e.InspectionTimestamp).HasColumnType("datetime");

                entity.Property(e => e.InstallationTimestamp).HasColumnType("datetime");

                entity.Property(e => e.ManufacturingTimestamp).HasColumnType("datetime");

                entity.Property(e => e.ReceivingTimestamp).HasColumnType("datetime");

                entity.Property(e => e.TagId).HasMaxLength(100);

                entity.Property(e => e.TaggingTimestamp).HasColumnType("datetime");

                // commented out since EF Core doesn't have option to disable proxy creation
                //entity.HasOne(d => d.Component)
                //    .WithMany(p => p.ComponentItems)
                //    .HasForeignKey(d => d.ComponentId)
                //    .HasConstraintName("FK_dbo.ComponentItems_dbo.Components_ComponentId");

                // will only reference from componentitems with it's component
                entity.HasOne(d => d.Component);
            });

            modelBuilder.Entity<Components>(entity =>
            {
                entity.HasKey(e => e.ComponentId);

                entity.HasIndex(e => e.ComponentTypeComponentTypeId)
                    .HasName("IX_ComponentType_ComponentTypeId");

                entity.HasIndex(e => e.ContractContractId)
                    .HasName("IX_Contract_ContractId");

                entity.HasIndex(e => e.ContractManufacturerId)
                    .HasName("IX_ContractManufacturerId");

                entity.Property(e => e.ComponentTypeComponentTypeId).HasColumnName("ComponentType_ComponentTypeId");

                entity.Property(e => e.ContractContractId).HasColumnName("Contract_ContractId");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.ComponentTypeComponentType)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.ComponentTypeComponentTypeId)
                    .HasConstraintName("FK_dbo.Components_dbo.ComponentTypes_ComponentType_ComponentTypeId");

                entity.HasOne(d => d.ContractContract)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.ContractContractId)
                    .HasConstraintName("FK_dbo.Components_dbo.Contracts_Contract_ContractId");

                entity.HasOne(d => d.ContractManufacturer)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.ContractManufacturerId)
                    .HasConstraintName("FK_dbo.Components_dbo.ContractManufacturers_ContractManufacturerId");
            });

            modelBuilder.Entity<ComponentTypes>(entity =>
            {
                entity.HasKey(e => e.ComponentTypeId);
            });

            modelBuilder.Entity<ContractManufacturers>(entity =>
            {
                entity.HasKey(e => e.ContractManufacturerId);

                entity.HasIndex(e => e.ComponentTypeId)
                    .HasName("IX_ComponentTypeId");

                entity.HasIndex(e => e.ContractId)
                    .HasName("IX_ContractId");

                entity.HasIndex(e => e.ManufacturerId)
                    .HasName("IX_ManufacturerId");

                entity.HasOne(d => d.ComponentType)
                    .WithMany(p => p.ContractManufacturers)
                    .HasForeignKey(d => d.ComponentTypeId)
                    .HasConstraintName("FK_dbo.ContractManufacturers_dbo.ComponentTypes_ComponentTypeId");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractManufacturers)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_dbo.ContractManufacturers_dbo.Contracts_ContractId");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.ContractManufacturers)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK_dbo.ContractManufacturers_dbo.Manufacturers_ManufacturerId");
            });

            modelBuilder.Entity<Contractors>(entity =>
            {
                entity.HasKey(e => e.ContractorId);

                entity.Property(e => e.ContractorName).IsRequired();
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.HasIndex(e => e.ContractorId)
                    .HasName("IX_ContractorId");

                entity.Property(e => e.ContractNo).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ContractorId)
                    .HasConstraintName("FK_dbo.Contracts_dbo.Contractors_ContractorId");
            });

            modelBuilder.Entity<Manufacturers>(entity =>
            {
                entity.HasKey(e => e.ManufacturerId);

                entity.Property(e => e.ManufacturerName).IsRequired();
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });
        }
    }
}
