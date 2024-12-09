using Microsoft.AspNetCore.Mvc;

namespace TravelExpertsMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
