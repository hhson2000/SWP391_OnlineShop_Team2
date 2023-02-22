using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;
using NitStore.Models.Domain;
using NitStore.Models.DTO;
using PagedList;

namespace NitStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly NitDbContext dbContext;

        public UsersController(NitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> ViewAllUser()
        {
            var userList = await dbContext.users.ToListAsync();
            return View(userList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDTO dto)
        {
            List<Role> roleList = new List<Role>();
            //roleList = = await dbContext.
            var User = new User()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Role = 1,
                IsActive = dto.IsActive,
                NeedToChange = true,
                Password = "123456"
            };

            dbContext.users.Add(User);
            dbContext.SaveChanges();
            return RedirectToAction("ViewAllUser", "Users", new { area = "" });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = dbContext.users.Where(x => x.Id == id).First();
            if (user != null)
            {
                dbContext.users.Remove(user);
                dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ViewAllUser", "Users", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = dbContext.users.Where(x => x.Id == id).First();

            if (user != null)
            {
                return View(user);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(User dto)
        {
            User currentUser = dbContext.users.Where(x => x.Id == dto.Id).First();
            currentUser.UserName = dto.UserName;
            currentUser.IsActive = dto.IsActive;
            //currentUser.NeedToChange = true;
            //currentUser.Email = dto.UserName;
            //currentUser.Password = "123456";
            dbContext.SaveChangesAsync();
            return RedirectToAction("ViewAllUser", "Users", new { area = "" });
        }
    }
}
