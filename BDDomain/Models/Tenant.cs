using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Tenant
{
    public int TenantId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual User? UpdatedByNavigation { get; set; }
}
