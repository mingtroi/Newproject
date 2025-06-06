using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Leaf
{
    public int LeaveId { get; set; }

    public int EmployeeId { get; set; }

    public string LeaveType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? TotalDays { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public virtual Employee? ApprovedByNavigation { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
