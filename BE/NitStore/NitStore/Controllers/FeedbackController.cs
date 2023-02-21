using Microsoft.AspNetCore.Mvc;

namespace NitStore.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
