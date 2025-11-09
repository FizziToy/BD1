using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int InvoiceId { get; set; }

    public DateOnly PaidDate { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
