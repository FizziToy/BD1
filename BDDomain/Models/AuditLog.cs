using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class AuditLog
{
    public long AuditId { get; set; }

    public string TableName { get; set; } = null!;

    public string KeyValue { get; set; } = null!;

    public string Operation { get; set; } = null!;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public DateTime ChangedAt { get; set; }

    public int? ChangedBy { get; set; }
}
