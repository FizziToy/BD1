using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Ownership
{
    public int OwnershipId { get; set; }

    public int UnitId { get; set; }

    public int OwnerId { get; set; }

    public decimal Share { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Owner Owner { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
