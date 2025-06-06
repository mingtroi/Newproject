namespace WebClient.Models
{
    public class DepartmentDTO
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }
}
