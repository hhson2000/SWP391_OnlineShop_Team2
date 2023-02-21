using Microsoft.AspNetCore.Mvc;
using NitStore.Data;

namespace NitStore.Controllers
{
    public class MaketerController : Controller
    {
        private readonly NitDbContext dbContext;
        public MaketerController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> HomeMaketer()
        {
            return View();
        }
        public async Task<IActionResult> ViewAllProduct()
        {
            return View();
        }
    }
}
