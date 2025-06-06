using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class LeaveBalance
{
    public int EmployeeId { get; set; }

    public int? AnnualLeave { get; set; }

    public int? SickLeave { get; set; }

    public int? UnpaidLeave { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
