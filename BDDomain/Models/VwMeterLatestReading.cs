using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class VwMeterLatestReading
{
    public int MeterId { get; set; }

    public DateOnly? LastDate { get; set; }
}
