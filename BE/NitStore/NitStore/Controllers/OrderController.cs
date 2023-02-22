using Microsoft.AspNetCore.Mvc;

namespace NitStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
