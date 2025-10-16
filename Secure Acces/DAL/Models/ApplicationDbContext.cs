using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessMethod> AccessMethods { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Door> Doors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=mssqlstud.fhict.local;Database=dbi550793_secuaccess;User Id=dbi550793_secuaccess;Password=secureaccess;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessMethod>(entity =>
        {
            entity.HasKey(e => e.AccessMethodId).HasName("PK__Access_M__1B2D72CD31CBE88D");

            entity.Property(e => e.AccessMethodId).ValueGeneratedNever();
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId).HasName("PK__Audit_Lo__6031F9F8DF89EA44");

            entity.Property(e => e.AuditLogId).ValueGeneratedNever();

            entity.HasOne(d => d.AccessMethod).WithMany(p => p.AuditLogs).HasConstraintName("FK_access_method_id");

            entity.HasOne(d => d.Location).WithMany(p => p.AuditLogs).HasConstraintName("FK_location_id2");
        });

        modelBuilder.Entity<Door>(entity =>
        {
            entity.HasKey(e => e.DoorId).HasName("PK__DOOR__179A5D111F90A8D7");

            entity.Property(e => e.DoorId).ValueGeneratedNever();

            entity.HasOne(d => d.Location).WithMany(p => p.Doors).HasConstraintName("FK_location_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA896F20DEF");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            entity.HasOne(d => d.Location).WithMany(p => p.Employees).HasConstraintName("FK_location");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees).HasConstraintName("FK_RoleId");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Location1).ValueGeneratedNever();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC7D12B4D5");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
