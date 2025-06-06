using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateOnly StartDate { get; set; }

    public string? ProfilePicture { get; set; }

    public string? Status { get; set; }

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Leaf> LeafApprovedByNavigations { get; set; } = new List<Leaf>();

    public virtual ICollection<Leaf> LeafEmployees { get; set; } = new List<Leaf>();

    public virtual LeaveBalance? LeaveBalance { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
