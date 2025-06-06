using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class ActivityLog
{
    public int LogId { get; set; }

    public int EmployeeId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
