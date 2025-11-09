using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class MeterReading
{
    public int ReadingId { get; set; }

    public int MeterId { get; set; }

    public DateOnly ReadingDate { get; set; }

    public decimal Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Meter Meter { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
