namespace WebClient.Models
{
    public class SalaryDTO
    {
        public int SalaryId { get; set; }

        public int EmployeeId { get; set; }

        public decimal BaseSalary { get; set; }

        public decimal? Allowance { get; set; }

        public decimal? Bonus { get; set; }

        public decimal? TotalIncome { get; set; }

        public DateOnly? SalaryDate { get; set; }

        public DateOnly? PaymentDate { get; set; }

        public bool? Paid { get; set; }

        public virtual EmployeeDTO Employee { get; set; } = null!;
    }
}
