using Microsoft.AspNetCore.Mvc;

namespace Homework3.Web.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}