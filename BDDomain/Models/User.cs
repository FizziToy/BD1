using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string Login { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Building> BuildingCreatedByNavigations { get; set; } = new List<Building>();

    public virtual ICollection<Building> BuildingUpdatedByNavigations { get; set; } = new List<Building>();

    public virtual ICollection<Invoice> InvoiceCreatedByNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceUpdatedByNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Lease> LeaseCreatedByNavigations { get; set; } = new List<Lease>();

    public virtual ICollection<Lease> LeaseUpdatedByNavigations { get; set; } = new List<Lease>();

    public virtual ICollection<Meter> MeterCreatedByNavigations { get; set; } = new List<Meter>();

    public virtual ICollection<MeterReading> MeterReadingCreatedByNavigations { get; set; } = new List<MeterReading>();

    public virtual ICollection<MeterReading> MeterReadingUpdatedByNavigations { get; set; } = new List<MeterReading>();

    public virtual ICollection<Meter> MeterUpdatedByNavigations { get; set; } = new List<Meter>();

    public virtual ICollection<Owner> OwnerCreatedByNavigations { get; set; } = new List<Owner>();

    public virtual ICollection<Owner> OwnerUpdatedByNavigations { get; set; } = new List<Owner>();

    public virtual ICollection<Ownership> OwnershipCreatedByNavigations { get; set; } = new List<Ownership>();

    public virtual ICollection<Ownership> OwnershipUpdatedByNavigations { get; set; } = new List<Ownership>();

    public virtual ICollection<Payment> PaymentCreatedByNavigations { get; set; } = new List<Payment>();

    public virtual ICollection<Payment> PaymentUpdatedByNavigations { get; set; } = new List<Payment>();

    public virtual ICollection<Property> PropertyCreatedByNavigations { get; set; } = new List<Property>();

    public virtual ICollection<Property> PropertyUpdatedByNavigations { get; set; } = new List<Property>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<ServiceRequest> ServiceRequestCreatedByNavigations { get; set; } = new List<ServiceRequest>();

    public virtual ICollection<ServiceRequest> ServiceRequestUpdatedByNavigations { get; set; } = new List<ServiceRequest>();

    public virtual ICollection<Tenant> TenantCreatedByNavigations { get; set; } = new List<Tenant>();

    public virtual ICollection<Tenant> TenantUpdatedByNavigations { get; set; } = new List<Tenant>();

    public virtual ICollection<Unit> UnitCreatedByNavigations { get; set; } = new List<Unit>();

    public virtual ICollection<Unit> UnitUpdatedByNavigations { get; set; } = new List<Unit>();

    public virtual ICollection<Vendor> VendorCreatedByNavigations { get; set; } = new List<Vendor>();

    public virtual ICollection<Vendor> VendorUpdatedByNavigations { get; set; } = new List<Vendor>();

    public virtual ICollection<WorkOrder> WorkOrderCreatedByNavigations { get; set; } = new List<WorkOrder>();

    public virtual ICollection<WorkOrder> WorkOrderUpdatedByNavigations { get; set; } = new List<WorkOrder>();
}
