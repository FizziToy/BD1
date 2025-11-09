using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public int BuildingId { get; set; }

    public string UnitNumber { get; set; } = null!;

    public decimal Area { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual ICollection<Meter> Meters { get; set; } = new List<Meter>();

    public virtual ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    public virtual User? UpdatedByNavigation { get; set; }
}
