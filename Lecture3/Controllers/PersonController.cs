using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture3.Controllers
{
    public class PersonController : Controller
    {
        // GET: PersonController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: PersonController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController1/Create
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

        // GET: PersonController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonController1/Edit/5
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

        // GET: PersonController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonController1/Delete/5
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
