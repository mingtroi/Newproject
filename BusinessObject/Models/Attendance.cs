using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly WorkDate { get; set; }

    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
