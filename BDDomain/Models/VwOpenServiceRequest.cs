using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class VwOpenServiceRequest
{
    public int RequestId { get; set; }

    public int UnitId { get; set; }

    public string Description { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
