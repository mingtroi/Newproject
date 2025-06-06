using Microsoft.AspNetCore.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class SalaryController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            var salary = APIFunction.GetAllSalaries();
            return View(salary);
        }
        [HttpPost]
        public IActionResult CreateSalaries(SalaryDTO salary)
        {
            //if (ModelState.IsValid)
            //{
                APIFunction.CreateSalaries(salary);
                return RedirectToAction("List");
            //}
            //return View(salary);
        }

    }
}
