using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BDDomain.Models;

public partial class RealEstateDbContext : DbContext
{
    public RealEstateDbContext()
    {
    }

    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<Meter> Meters { get; set; }

    public virtual DbSet<MeterReading> MeterReadings { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Ownership> Ownerships { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VwActiveLease> VwActiveLeases { get; set; }

    public virtual DbSet<VwActiveUnit> VwActiveUnits { get; set; }

    public virtual DbSet<VwCurrentLease> VwCurrentLeases { get; set; }

    public virtual DbSet<VwDeletedEntity> VwDeletedEntities { get; set; }

    public virtual DbSet<VwMeterLatestReading> VwMeterLatestReadings { get; set; }

    public virtual DbSet<VwOpenServiceRequest> VwOpenServiceRequests { get; set; }

    public virtual DbSet<VwOutstandingInvoice> VwOutstandingInvoices { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WIN-BRGMI9R4JDD;Database=RealEstateDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId);

            entity.ToTable("AuditLog");

            entity.Property(e => e.ChangedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.KeyValue).HasMaxLength(256);
            entity.Property(e => e.Operation).HasMaxLength(20);
            entity.Property(e => e.TableName).HasMaxLength(128);
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.ToTable("Building");

            entity.HasIndex(e => e.PropertyId, "IX_Building_PropertyId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BuildingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Building_CreatedBy");

            entity.HasOne(d => d.Property).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Building_Property");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.BuildingUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Building_UpdatedBy");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(100);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice");

            entity.HasIndex(e => new { e.LeaseId, e.DueDate }, "IX_Invoice_Lease_DueDate");

            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InvoiceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Invoice_CreatedBy");

            entity.HasOne(d => d.Lease).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.LeaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Lease");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InvoiceUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Invoice_UpdatedBy");
        });

        modelBuilder.Entity<Lease>(entity =>
        {
            entity.ToTable("Lease", tb =>
                {
                    tb.HasTrigger("trg_Lease_Audit");
                    tb.HasTrigger("trg_Lease_ValidateDates");
                });

            entity.HasIndex(e => e.TenantId, "IX_Lease_TenantId");

            entity.HasIndex(e => new { e.UnitId, e.TenantId }, "IX_Lease_Unit_Tenant").HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.RentAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LeaseCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Lease_CreatedBy");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Leases)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lease_Tenant");

            entity.HasOne(d => d.Unit).WithMany(p => p.Leases)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lease_Unit");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.LeaseUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Lease_UpdatedBy");
        });

        modelBuilder.Entity<Meter>(entity =>
        {
            entity.ToTable("Meter");

            entity.HasIndex(e => e.SerialNumber, "UQ__Meter__048A0008584F391B").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MeterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Meter_CreatedBy");

            entity.HasOne(d => d.Unit).WithMany(p => p.Meters)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meter_Unit");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MeterUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Meter_UpdatedBy");
        });

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasKey(e => e.ReadingId);

            entity.ToTable("MeterReading");

            entity.HasIndex(e => new { e.MeterId, e.ReadingDate }, "UQ_MR").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Value).HasColumnType("decimal(12, 3)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MeterReadingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_MR_CreatedBy");

            entity.HasOne(d => d.Meter).WithMany(p => p.MeterReadings)
                .HasForeignKey(d => d.MeterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MR_Meter");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.MeterReadingUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_MR_UpdatedBy");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.ToTable("Owner");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OwnerCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Owner_CreatedBy");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OwnerUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Owner_UpdatedBy");
        });

        modelBuilder.Entity<Ownership>(entity =>
        {
            entity.ToTable("Ownership");

            entity.HasIndex(e => new { e.UnitId, e.OwnerId }, "UQ_Ownership").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Share).HasColumnType("decimal(5, 4)");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OwnershipCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Ownership_CreatedBy");

            entity.HasOne(d => d.Owner).WithMany(p => p.Ownerships)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ownership_Owner");

            entity.HasOne(d => d.Unit).WithMany(p => p.Ownerships)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ownership_Unit");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OwnershipUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Ownership_UpdatedBy");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment", tb => tb.HasTrigger("trg_Payment_UpdateInvoice"));

            entity.HasIndex(e => e.InvoiceId, "IX_Payment_Invoice");

            entity.HasIndex(e => e.PaidDate, "IX_Payment_PaidDate");

            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PaymentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Payment_CreatedBy");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Invoice");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PaymentUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Payment_UpdatedBy");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("Property", tb => tb.HasTrigger("trg_SetAudit_Property"));

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PropertyCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Property_CreatedBy");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PropertyUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Property_UpdatedBy");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ServiceRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.ToTable("ServiceRequest");

            entity.HasIndex(e => e.Status, "IX_ServiceRequest_Status");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ServiceRequestCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_SR_CreatedBy");

            entity.HasOne(d => d.Unit).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SR_Unit");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ServiceRequestUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_SR_UpdatedBy");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.ToTable("Tenant", tb => tb.HasTrigger("trg_SetAudit_Tenant"));

            entity.HasIndex(e => e.IsDeleted, "IX_Tenant_Active").HasFilter("([IsDeleted]=(0))");

            entity.HasIndex(e => e.Phone, "IX_Tenant_Phone");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TenantCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Tenant_CreatedBy");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TenantUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Tenant_UpdatedBy");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("Unit", tb =>
                {
                    tb.HasTrigger("trg_SetAudit_Unit");
                    tb.HasTrigger("trg_Unit_SoftDelete_Lease");
                    tb.HasTrigger("trg_Unit_UpdateAudit");
                });

            entity.HasIndex(e => e.BuildingId, "IX_Unit_Building_Active").HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.Area).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UnitNumber).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Building).WithMany(p => p.Units)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Unit_Building");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UnitCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Unit_CreatedBy");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.UnitUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Unit_UpdatedBy");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "UQ__User__5E55825B863A2BA1").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.ToTable("Vendor");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VendorCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Vendor_CreatedBy");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.VendorUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Vendor_UpdatedBy");
        });

        modelBuilder.Entity<VwActiveLease>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ActiveLeases");

            entity.Property(e => e.RentAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TenantName).HasMaxLength(200);
            entity.Property(e => e.UnitNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<VwActiveUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwActiveUnits");

            entity.Property(e => e.Area).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt).HasPrecision(0);
            entity.Property(e => e.UnitNumber).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
        });

        modelBuilder.Entity<VwCurrentLease>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwCurrentLeases");

            entity.Property(e => e.CreatedAt).HasPrecision(0);
            entity.Property(e => e.RentAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
        });

        modelBuilder.Entity<VwDeletedEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_DeletedEntities");

            entity.Property(e => e.EntityName)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
        });

        modelBuilder.Entity<VwMeterLatestReading>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwMeterLatestReadings");
        });

        modelBuilder.Entity<VwOpenServiceRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwOpenServiceRequests");

            entity.Property(e => e.CreatedAt).HasPrecision(0);
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.RequestId).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
        });

        modelBuilder.Entity<VwOutstandingInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwOutstandingInvoices");

            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt).HasPrecision(0);
            entity.Property(e => e.InvoiceId).ValueGeneratedOnAdd();
            entity.Property(e => e.Outstanding).HasColumnType("decimal(13, 2)");
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.ToTable("WorkOrder");

            entity.HasIndex(e => new { e.RequestId, e.Status }, "IX_WO_Request_Status").HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.Cost).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WorkOrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_WO_CreatedBy");

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_WO_Employee");

            entity.HasOne(d => d.Request).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WO_Request");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.WorkOrderUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_WO_UpdatedBy");

            entity.HasOne(d => d.Vendor).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_WO_Vendor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
