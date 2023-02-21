using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;

namespace NitStore.Controllers
{
    public class ProductController : Controller
    {

        private readonly NitDbContext dbContext;

        public ProductController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> ViewAllProduct()
        {
            
            return View();
        }
    }
}
