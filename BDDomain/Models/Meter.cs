using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class Meter
{
    public int MeterId { get; set; }

    public int UnitId { get; set; }

    public string Type { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public DateOnly InstalledOn { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<MeterReading> MeterReadings { get; set; } = new List<MeterReading>();

    public virtual Unit Unit { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
