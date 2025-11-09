using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class WorkOrder
{
    public int WorkOrderId { get; set; }

    public int RequestId { get; set; }

    public int? VendorId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal Cost { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ServiceRequest Request { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual Vendor? Vendor { get; set; }
}
