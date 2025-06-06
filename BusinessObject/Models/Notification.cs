using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual Employee? Employee { get; set; }
}
