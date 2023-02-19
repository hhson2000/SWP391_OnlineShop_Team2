using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;

namespace NitStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly NitDbContext dbContext;
        public AdminController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> HomeAdmin()
        {
            return View();
        }
        public async Task<IActionResult> ViewAllUser()
        {
            var userList = await dbContext.users.ToListAsync();
            return View(userList);
        }
        
    }
}
