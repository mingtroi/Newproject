using AutoMapper;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN232PRJ.ViewModel;

namespace PRN232PRJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly NewHrmanagementContext _context;
        private readonly IMapper _mapper;
        public DepartmentsController(NewHrmanagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetAllDepartments")]
        public IActionResult GetAllDepartments()
        {
            try
            {
                var departments = _context.Departments.ToList();
                if (departments == null || !departments.Any())
                {
                    return NotFound("No department found.");
                }
                var departmentDtos = _mapper.Map<List<DepartmentVM>>(departments);
                return Ok(departmentDtos);
                // Assuming you have a CategoryVM class for mapping
                //var categoryDtos = _mapper.Map<List<CategoryVM>>(categories);
                //return Ok(categoryDtos);
                //return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetDepartmentById/{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var departments = _context.Departments.Include(x => x.Employees).FirstOrDefault(x => x.DepartmentId == id);
                if (departments == null)
                {
                    return NotFound("Department not found.");
                }
                var departmentDto = _mapper.Map<DepartmentVM>(departments);
                return Ok(departmentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreateDepartment")]
        public IActionResult CreateDepartment(DepartmentVM department)
        {
                Department newDepartment = _mapper.Map<Department>(department);
                if (newDepartment == null)
                {
                    return BadRequest("Invalid department data.");
                }

                bool departmentExists = _context.Departments.Any(d => d.DepartmentName == newDepartment.DepartmentName);
                if (departmentExists)
                {
                    return BadRequest("Department with the same name already exists.");
                }
                _context.Departments.Add(newDepartment);
                _context.SaveChanges();
                return Ok("Department created successfully.");

        }
        [HttpPost("UpdateDepartment")]
        public IActionResult UpdateDepartment(DepartmentVM department)
        {
            try
            {
                Department newDepartment = _mapper.Map<Department>(department);
                if (newDepartment == null) {
                    return BadRequest("Invalid department data.");
                }
                var existingDepartment = _context.Departments.FirstOrDefault(d => d.DepartmentId == newDepartment.DepartmentId);
                if (existingDepartment == null)
                {
                    return NotFound("Department not found.");
                }
                existingDepartment.DepartmentName = newDepartment.DepartmentName;
                existingDepartment.Description = newDepartment.Description;
                _context.SaveChanges();
                return Ok();
                //if (department == null)
                //{
                //    return BadRequest("Invalid department data.");
                //}
                //var existingDepartment = _context.Departments.Find(department.DepartmentId);
                //if (existingDepartment == null)
                //{
                //    return NotFound("Department not found.");
                //}
                //existingDepartment.DepartmentName = department.DepartmentName;
                //existingDepartment.Description = department.Description;
                //_context.SaveChanges();
                //return Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteDepartment/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                var department = _context.Departments.FirstOrDefault(c => c.DepartmentId == id);
                if(department == null)
                {
                    return NotFound("Department not found.");
                }
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddEmployeeToDepartment/{employeeId}/{departmentId}")]
        public IActionResult AddEmployeeToDepartment(int departmentId, int employeeId)
        {
            var department = _context.Departments.Include(d => d.Employees)
                                                  .FirstOrDefault(d => d.DepartmentId == departmentId);
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (department == null || employee == null)
            {
                return NotFound("Department or Employee not found.");
            }
            if(department.Employees.Contains(employee))
            {
                return BadRequest("Employee is already in this department.");
            }
            department.Employees.Add(employee);
            _context.SaveChanges();
            return Ok("Employee added to department.");
        }

        [HttpPost("RemoveEmployeeFromDepartment/{employeeId}/{departmentId}")]
        public IActionResult RemoveEmployeeFromDepartment(int departmentId, int employeeId)
        {
            var department = _context.Departments.Include(d => d.Employees)
                                                   .FirstOrDefault(d => d.DepartmentId == departmentId);
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (department == null || employee == null)
            {
                return NotFound("Department or Employee not found.");
            }
            if (!department.Employees.Contains(employee))
            {
                return BadRequest("Employee is not in this department.");
            }
            department.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok("Employee removed from department.");
        }
    }
}
