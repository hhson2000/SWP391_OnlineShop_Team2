using Microsoft.AspNetCore.Mvc;
using NitStore.Data;

namespace NitStore.Controllers
{
    public class AuthenController : Controller
    {
        private readonly NitDbContext dbContext;
        public AuthenController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
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
