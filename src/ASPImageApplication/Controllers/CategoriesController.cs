using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using ASPImageApplication.Models;
using Microsoft.AspNet.Authorization;

namespace ASPImageApplication.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(_context.Category.ToList());
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = _context.Category.Single(m => m.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Category.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = _context.Category.Single(m => m.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Category category = _context.Category.Single(m => m.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Category category = _context.Category.Single(m => m.CategoryId == id);
            _context.Category.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Display(string id)
        {
            var result = _context.Category.Where(i => i.Owner == id);
            return View(result.First().Name);
        }
    }
}
