using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class DepartmentController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            var departments = APIFunction.GetAllDepartments();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var department = APIFunction.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewBag.Employees = APIFunction.GetAllEmployee();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentDTO department)
        {
            int result = APIFunction.CreateDepartment(department);
            string message;
            if (result == 200)
            {
                message = "Department created successfully.";
            }
            else
            {
                message = "Failed to create department. Please try again.";
            }
            ViewBag.message = message;
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            DepartmentDTO depart = APIFunction.GetDepartmentById(id);
            if (depart == null)
            {
                return NotFound();
            }
            return View(depart);
        }
        [HttpPost]
        public IActionResult Update(DepartmentDTO newDepartment)
        {
            int result = APIFunction.UpdateDepartment(newDepartment);
            string message;
            if (result == 200)
            {
                message = "Employee updated successfully.";
            }
            else
            {
                message = "Failed to update employee. Please try again.";
            }
            ViewBag.message = message;
            //return View(newDepartment);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            int result = APIFunction.DeleteDepartment(id);
            string message;
            if (result == 200)
            {
                message = "Department deleted successfully.";
            }
            else
            {
                message = "Failed to delete department. Please try again.";
            }
            ViewBag.message = message;
            return RedirectToAction("List");
        }
        //[HttpGet]
        //public IActionResult ModifyEmployee(int id)
        //{
        //    var department = APIFunction.GetDepartmentById(id);
        //    ViewBag.Employees = APIFunction.GetAllEmployee();
        //    return View(department);
        //}
        //[HttpPost]
        //public IActionResult AddEmployeeToDepartment(int departmentId, int employeeId)
        //{
        //    var department = APIFunction.GetDepartmentById(departmentId);
        //    var employee = APIFunction.GetEmployeeById(employeeId);
        //    if (department == null || employee == null)
        //    {
        //        return NotFound();
        //    }
        //    int result = APIFunction.AddEmployeeToDepartment(departmentId, employeeId);
        //    return View(result);
        //}

        [HttpGet]
        public IActionResult ModifyEmployee(int id)
        {
            var department = APIFunction.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.Employees = APIFunction.GetAllEmployee(); // Toàn bộ nhân viên
            return View(department); // View dùng Model là DepartmentDTO
        }

        [HttpPost]
        public IActionResult AddEmployeeToDepartment(int employeeId , int departmentId)
        {
            var department = APIFunction.GetDepartmentById(departmentId);
            var employee =  APIFunction.GetAllEmployee()?.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (department == null || employee == null)
            {
                return NotFound();
            }
            int result = APIFunction.AddEmployeeToDepartment(employeeId, departmentId);
            ViewBag.Message = result == 200 ? "Employee added successfully." : "Failed to add employee.";
            return RedirectToAction("ModifyEmployee", new { id = departmentId }); // Trả về view với model là department
        }
        [HttpPost]
        public IActionResult RemoveEmployeeFromDepartment(int employeeId, int departmentId)
        {
            var department = APIFunction.GetDepartmentById(departmentId);
            var employee = APIFunction.GetAllEmployee()?.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (department == null || employee == null)
            {
                return NotFound();
            }
            int result = APIFunction.RemoveEmployeeFromDepartment(employeeId, departmentId);
            ViewBag.Message = result == 200 ? "Employee removed successfully." : "Failed to remove employee.";
            return RedirectToAction("ModifyEmployee", new { id = departmentId }); // Trả về view với model là department
        }
    }
}
