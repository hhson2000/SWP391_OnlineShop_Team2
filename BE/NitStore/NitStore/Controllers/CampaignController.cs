using Microsoft.AspNetCore.Mvc;

namespace NitStore.Controllers
{
    public class CampaignController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
