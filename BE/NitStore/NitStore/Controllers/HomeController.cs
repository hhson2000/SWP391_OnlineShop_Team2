using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NitStore.Models;
using NitStore.Data;

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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> HomeMaketer()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}