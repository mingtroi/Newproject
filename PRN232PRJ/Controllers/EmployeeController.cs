using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232PRJ.ViewModel;
using BusinessObject.Models;
namespace PRN232PRJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NewHrmanagementContext _context;
        private readonly IMapper _mapper;
        public EmployeeController(NewHrmanagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var employees = _context.Employees.ToList();
                if (employees == null || !employees.Any())
                {
                    return NotFound("No employees found.");
                }
                var employeeDtos = _mapper.Map<List<EmployeeVM>>(employees);
                return Ok(employeeDtos);
                //return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(EmployeeVM employee)
        {
            Employee em = _mapper.Map<Employee>(employee);

            bool phoneExists = _context.Employees.Any(e => e.PhoneNumber == em.PhoneNumber);
            if (phoneExists)
            {
                return BadRequest("Phone number already exists.");
            }

            _context.Employees.Add(em);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("GetEmployeeById/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }
            var employeeDto = _mapper.Map<EmployeeVM>(employee);
            return Ok(employeeDto);
        }

        [HttpPost("UpdateEmployee")]
        public IActionResult UpdateEmployee(EmployeeVM employee)
        {
            var existingEmployee = _context.Employees.Find(employee.EmployeeId);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found.");
            }
            _mapper.Map(employee, existingEmployee);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                return NotFound("Employee not found.");
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok();
        }
    }
}
