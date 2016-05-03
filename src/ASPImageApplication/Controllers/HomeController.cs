using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPImageApplication.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;

namespace ASPImageApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;
        public HomeController(ApplicationDbContext _context, ILogger<HomeController> logger)
        {
            this._context = _context;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            if(_context.Image.Where(i => i.Public).ToList() == null)
            {
                return View();
            } else
            {
                return View(_context.Image.Where(i => i.Public).ToList());
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult DisplayPublic(int dbId)
        {
            var result = _context.Image.Where(i => i.ImageId == dbId);
            return base.File(result.First().Data, result.First().MimeType);
        }

        [HttpPost, ActionName("Like")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Like(int imageId)
        {
            Image image = _context.Image.Single(m => m.ImageId == imageId);
            image.Likes = image.Likes + 1;
            Debug.WriteLine(image.Likes);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
