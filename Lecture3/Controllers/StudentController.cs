using Lecture3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture3.Controllers
{
    public class StudentController : Controller
    {
        private static List<StudentDetailsViewModel> _students = new List<StudentDetailsViewModel>
        {
            new StudentDetailsViewModel
            {
                StudentNumber = "12345678901",
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateOnly(2000, 5, 10),
                Email = "john.doe@example.com",
                PhoneNumber = "555-5555",
                YearOfStudy = 2,
                CourseName = "Computer Science",
                GPA = 3.8,
                EnrollmentDate = new DateOnly(2019, 9, 1)
            },
            new StudentDetailsViewModel
            {
                StudentNumber = "23456789012",
                FirstName = "Jane",
                LastName = "Smith",
                BirthDate = new DateOnly(2001, 3, 25),
                Email = "jane.smith@example.com",
                PhoneNumber = "555-5556",
                YearOfStudy = 3,
                CourseName = "Mechanical Engineering",
                GPA = 3.7,
                EnrollmentDate = new DateOnly(2018, 9, 1)
            }
        };
        // GET: StudentController
        public ActionResult Index()
        {
            return View(_students);
        }

        // GET: StudentController/Details/5 
        public IActionResult Details(string id)
        {
            var student = new StudentDetailsViewModel
            {
                StudentNumber = "23456789012",
                FirstName = "Jane",
                LastName = "Smith",
                BirthDate = new DateOnly(2001, 3, 25),
                Email = "jane.smith@example.com",
                PhoneNumber = "555-5556",
                YearOfStudy = 3,
                CourseName = "Mechanical Engineering",
                GPA = 3.7,
                EnrollmentDate = new DateOnly(2018, 9, 1)
            };
           
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
