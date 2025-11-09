using System;
using System.Collections.Generic;

namespace BDDomain.Models;

public partial class VwDeletedEntity
{
    public string EntityName { get; set; } = null!;

    public int Id { get; set; }

    public string? FullName { get; set; }

    public DateTime UpdatedAt { get; set; }
}
