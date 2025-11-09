using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class VwActiveLease
{
    public int LeaseId { get; set; }

    public string TenantName { get; set; } = null!;

    public string UnitNumber { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal RentAmount { get; set; }
}
