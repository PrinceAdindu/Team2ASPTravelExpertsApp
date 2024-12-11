using Microsoft.AspNetCore.Mvc;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            var context = new TravelExpertsContext();
            var agencies = context.Agencies.ToList();
            //var agents = context.Agents.ToList();
            return View(agencies);
        }
    }
}
