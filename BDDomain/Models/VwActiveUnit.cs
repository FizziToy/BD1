using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class VwActiveUnit
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

    public int PropertyId { get; set; }
}
