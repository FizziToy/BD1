using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class VwOutstandingInvoice
{
    public int InvoiceId { get; set; }

    public int LeaseId { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal Amount { get; set; }

    public decimal PaidAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public decimal? Outstanding { get; set; }
}
