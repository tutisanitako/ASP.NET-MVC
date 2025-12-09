using Microsoft.AspNetCore.Mvc;

namespace Homework3.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}