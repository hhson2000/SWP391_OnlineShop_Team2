using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NitStore.Data;
using NitStore.Models.Domain;
using NitStore.Models.DTO;

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

        [HttpPost]
        public async Task<IActionResult> Login(User loginUser)
        {
            if (loginUser != null)
            {
                string username = loginUser.UserName;
                string password = loginUser.Password;
                var user = await dbContext.users.Where(u => u.UserName.Equals(username) && u.Password.Equals(password)).FirstAsync();
                if(user != null)
                {
                    if(user.Role == 5)
                    {
                        return RedirectToAction("ListProduct", "Products", new { area = "" });
                    }
                    
                }
            } else
            {
                return View();
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO user)
        {
            if (user != null)
            {
                if (user.Password.Equals(user.RePassword)) {
                    User newUser = new User
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.Password,
                        Role = 5,
                        Status = 1
                    };
                    try
                    {
                        dbContext.users.Add(newUser);
                        await dbContext.SaveChangesAsync();
                        UserDetail userDetail = new UserDetail
                        {
                            Id = newUser.Id,
                            Name = user.Name,
                            Address = user.Address,
                            PhoneNumber = user.PhoneNumber,
                            Gender = user.Gender
                        };
                        dbContext.userDetail.Add(userDetail);
                        await dbContext.SaveChangesAsync();
                        return RedirectToAction("Login");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        return View();
                    }
                } else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
