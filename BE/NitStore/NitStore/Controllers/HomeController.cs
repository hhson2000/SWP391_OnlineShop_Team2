using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NitStore.Models;
using NitStore.Data;
using NitStore.Models.Domain;

namespace NitStore.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly NitDbContext dbContext;

        public HomeController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //return RedirectToAction("HomeAdmin", "Admin", new { area = "" });
            return RedirectToAction("LandingPage");
        }

        [HttpGet]
        public async Task<IActionResult> LandingPage()
        {
            List<Category> categories = new List<Category>();
            categories = dbContext.categories.ToList()/*.GetRange(0,5)*/;
            ViewBag.Categories = categories;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> HomeMaketer()
        {
            return View();
        }

        
    }
}