using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult List()
        {
            List<EmployeeDTO> employees = APIFunction.GetAllEmployee();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //var employee = new EmployeeDTO();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDTO employee)
        {

            int result = APIFunction.CreateEmployee(employee);
            string message;
            if (result == 200)
            {
                message = "Employee created successfully.";
            }
            else
            {
                message = "Failed to create employee. Please try again.";
            }
            ViewBag.message = message;
            //return View(employee);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            EmployeeDTO employee = APIFunction.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            EmployeeDTO employee = APIFunction.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(EmployeeDTO employee)
        {
            int result = APIFunction.UpdateEmployee(employee);
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
            //return View(employee);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            int result = APIFunction.DeleteEmployee(id);
            string message;
            if (result == 200)
            {
                message = "Employee deleted successfully.";
            }
            else
            {
                message = "Failed to delete employee. Please try again.";
            }
            ViewBag.message = message;
            return RedirectToAction("List");
        }
    }
}
