using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using ASPImageApplication.Models;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNet.Authorization;

namespace ASPImageApplication.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        private ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        public ImagesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Images
        public IActionResult Index()
        {
            var applicationDbContext = _context.Image.Include(i => i.Category);
            return View(applicationDbContext.ToList());
        }

        // GET: Images/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Image image = _context.Image.Single(m => m.ImageId == id);
            if (image == null)
            {
                return HttpNotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Category");
            return View();
        }

        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Image image, IFormFile file)
        {
            if (file == null || file.Length < 0)
            {
                ModelState.AddModelError("image_file", "File is invalid");
            }
            if (ModelState.IsValid)
            {
                image.FileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                image.MimeType = file.ContentType;
                image.Owner = _httpContextAccessor.HttpContext.User.Identity.Name;

                using (var stream = file.OpenReadStream())
                {
                    byte[] buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        image.Data = ms.ToArray();
                    }
                }
  
                _context.Image.Add(image);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Category", image.CategoryId);
            return View(image);
        }

        // GET: Images/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Image image = _context.Image.Single(m => m.ImageId == id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Category", image.CategoryId);
            return View(image);
        }

        // POST: Images/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                _context.Update(image);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Category", image.CategoryId);
            return View(image);
        }

        // GET: Images/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Image image = _context.Image.Single(m => m.ImageId == id);
            if (image == null)
            {
                return HttpNotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Image image = _context.Image.Single(m => m.ImageId == id);
            _context.Image.Remove(image);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Display(string id, int dbId)
        {
            var result = _context.Image.Where(i => i.ImageId == dbId && i.Owner == id);
            return base.File(result.First().Data, result.First().MimeType);
        }
    }
}
