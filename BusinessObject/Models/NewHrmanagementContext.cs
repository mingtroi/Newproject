using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Models;

public partial class NewHrmanagementContext : DbContext
{
    public NewHrmanagementContext()
    {
    }

    public NewHrmanagementContext(DbContextOptions<NewHrmanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Backup> Backups { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Leaf> Leaves { get; set; }

    public virtual DbSet<LeaveBalance> LeaveBalances { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =localhost; database = NewHRManagement ;uid=sa;pwd=123;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Activity__5E5499A890312B50");

            entity.ToTable("ActivityLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ActivityL__Emplo__44FF419A");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263CC6360DAF");

            entity.ToTable("Attendance");

            entity.HasIndex(e => new { e.EmployeeId, e.WorkDate }, "UQ_Attendance_Employee_Date").IsUnique();

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Note).HasMaxLength(250);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Attendanc__Emplo__398D8EEE");
        });

        modelBuilder.Entity<Backup>(entity =>
        {
            entity.HasKey(e => e.BackupId).HasName("PK__Backups__EB9069E2258529F1");

            entity.Property(e => e.BackupId).HasColumnName("BackupID");
            entity.Property(e => e.BackupDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD58E066AC");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC34C8172458").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF143E9569B");

            entity.ToTable(tb => tb.HasTrigger("trg_AutoCreateLeaveBalance"));

            entity.HasIndex(e => e.FullName, "IX_Employees_FullName");

            entity.HasIndex(e => e.Position, "IX_Employees_Position");

            entity.HasIndex(e => e.Username, "UQ__Employee__536C85E4C6899141").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__85FB4E386E6EC58E").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.ProfilePicture).HasMaxLength(250);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValue("Active");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasMany(d => d.Departments).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeDepartment",
                    r => r.HasOne<Department>().WithMany()
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK__EmployeeD__Depar__2F10007B"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK__EmployeeD__Emplo__2E1BDC42"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "DepartmentId").HasName("PK__Employee__B1F0364D6D320B63");
                        j.ToTable("EmployeeDepartments");
                        j.IndexerProperty<int>("EmployeeId").HasColumnName("EmployeeID");
                        j.IndexerProperty<int>("DepartmentId").HasColumnName("DepartmentID");
                    });
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6A0D19736");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Leaf>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__Leaves__796DB97955007CA3");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateLeaveBalance"));

            entity.Property(e => e.LeaveId).HasColumnName("LeaveID");
            entity.Property(e => e.ApprovalDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LeaveType).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalDays).HasComputedColumnSql("(datediff(day,[StartDate],[EndDate])+(1))", true);

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.LeafApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Leaves_ApprovedBy");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeafEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Leaves__Employee__4D94879B");
        });

        modelBuilder.Entity<LeaveBalance>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__LeaveBal__7AD04FF1F0E07076");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.AnnualLeave).HasDefaultValue(12);
            entity.Property(e => e.SickLeave).HasDefaultValue(5);
            entity.Property(e => e.UnpaidLeave).HasDefaultValue(0);

            entity.HasOne(d => d.Employee).WithOne(p => p.LeaveBalance)
                .HasForeignKey<LeaveBalance>(d => d.EmployeeId)
                .HasConstraintName("FK__LeaveBala__Emplo__5441852A");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E328C17132C");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Employee).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Notificat__Emplo__412EB0B6");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48E5474D2F9B");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReportType).HasMaxLength(50);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Salaries__4BE204B72922CB75");

            entity.Property(e => e.SalaryId).HasColumnName("SalaryID");
            entity.Property(e => e.Allowance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bonus)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Paid).HasDefaultValue(false);
            entity.Property(e => e.SalaryDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TotalIncome)
                .HasComputedColumnSql("(([BaseSalary]+[Allowance])+[Bonus])", true)
                .HasColumnType("decimal(20, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Salaries__Employ__35BCFE0A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
