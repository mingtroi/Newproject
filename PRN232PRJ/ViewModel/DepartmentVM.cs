using BusinessObject.Models;

namespace PRN232PRJ.ViewModel
{
    public class DepartmentVM
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
        public List <Employee> Employees { get; set; } = new List<Employee>();

    }
}
