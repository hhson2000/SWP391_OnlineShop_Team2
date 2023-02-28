using Microsoft.AspNetCore.Mvc;

namespace NitStore.Controllers
{
    public class AuthenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
