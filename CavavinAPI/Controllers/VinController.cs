using Microsoft.AspNetCore.Mvc;

namespace CavavinAPI.Controllers
{
    public class VinController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
