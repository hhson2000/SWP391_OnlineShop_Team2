using Microsoft.AspNetCore.Mvc;

namespace NitStore.Controllers
{
    public class SliderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
