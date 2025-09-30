using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture2.Controllers
{
    public class ListController1 : Controller
    {
        // GET: ListController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: ListController1/Details/5

        [HttpGet("get-details/{category}/{page?}")]
        public ActionResult Details(string category, int page = 1)
        {
            return View();
        }

        // GET: ListController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListController1/Create
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

        // GET: ListController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListController1/Edit/5
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

        // GET: ListController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListController1/Delete/5
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
