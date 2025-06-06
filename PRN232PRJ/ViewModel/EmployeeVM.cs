namespace PRN232PRJ.ViewModel
{
    public class EmployeeVM
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

        //public string? Status { get; set; }

        public string Role { get; set; } = null!;

        //public DateTime? CreatedAt { get; set; }
    }
}
