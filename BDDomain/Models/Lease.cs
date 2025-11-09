using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Lease
{
    public int LeaseId { get; set; }

    public int UnitId { get; set; }

    public int TenantId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal RentAmount { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
