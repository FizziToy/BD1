using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Owner
{
    public int OwnerId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();

    public virtual User? UpdatedByNavigation { get; set; }
}
