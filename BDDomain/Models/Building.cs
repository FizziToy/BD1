using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Building
{
    public int BuildingId { get; set; }

    public int PropertyId { get; set; }

    public string Name { get; set; } = null!;

    public int Floors { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Property Property { get; set; } = null!;

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();

    public virtual User? UpdatedByNavigation { get; set; }
}
