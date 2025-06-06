using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using PRN232PRJ.ViewModel;

namespace PRN232PRJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly NewHrmanagementContext _context;
        private readonly IMapper _mapper;
        public SalaryController(NewHrmanagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllSalaries")]
        public IActionResult GetAllSalaries()
        {
            try
            {
                var salaries = _context.Salaries.Include(s => s.Employee).ToList();
                return Ok(salaries);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetSalaryById/{id}")]
        public IActionResult GetSalaryById(int id)
        {
            var salary = _context.Salaries.Include(x => x.Employee).FirstOrDefault(s => s.SalaryId == id);
            if (salary == null)
            {
                return NotFound($"Salary with ID {id} not found.");
            }
            return Ok(salary);
        }

        [HttpGet("GetSalariesByEmployeeId/{id}")]
        public IActionResult GetSalariesByEmployeeId(int id)
        {
            var salaries = _context.Salaries.Include(s => s.Employee).Where(s => s.EmployeeId == id).ToList();
            if (salaries == null || !salaries.Any())
            {
                return NotFound($"No salaries found for employee with ID {id}.");
            }
            return Ok(salaries);
        }

        [HttpPost("CreateSalaries")]
        public IActionResult CreateSalaries()
        {
            try
            {
                var employee = _context.Employees.ToList();
                foreach (var emp in employee)
                {
                    bool alreadyExists = _context.Salaries.Any(s =>
                        s.EmployeeId == emp.EmployeeId);

                    if (!alreadyExists)
                    {
                        var newSalary = new Salary
                        {
                            EmployeeId = emp.EmployeeId,
                            BaseSalary = emp.Salary,
                            Allowance = 0,
                            Bonus = 0,
                            Paid = false,
                            SalaryDate = DateOnly.MaxValue,


                        };
                        decimal baseAmount = newSalary.BaseSalary;
                        newSalary.TotalIncome = baseAmount;

                        _context.Salaries.Add(newSalary);

                    }
                }
                _context.SaveChanges();
                return Ok("Salaries created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("UpdateSalaries")]
        public IActionResult UpdateSalaries(SalaryVM salary, int id)
        {
            var existingSalary = _context.Salaries.FirstOrDefault(x => x.SalaryId == id);
            if(existingSalary == null)
            {
                return BadRequest();
            }
            existingSalary.BaseSalary = salary.BaseSalary;
            existingSalary.Allowance = salary.Allowance;
            existingSalary.Bonus = salary.Bonus;
            existingSalary.TotalIncome = salary.BaseSalary + salary.Allowance + salary.Bonus;
            _context.Salaries.Update(existingSalary);
            _context.SaveChanges();
            return Ok("Salary updated successfully.");
        }
        [HttpPost("PaySalary")]
        public IActionResult PaySalary(int id)
        {
            var salary = _context.Salaries.FirstOrDefault(s => s.SalaryId == id);
            if (salary == null)
            {
                return NotFound($"Salary with ID {id} not found.");
            }
            if (salary.Paid == true)
            {
                return BadRequest("Salary has already been paid.");
            }
            salary.Paid = true;
            salary.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
            _context.Salaries.Update(salary);
            _context.SaveChanges();
            return Ok("Salary paid successfully.");
        }
    }
}
