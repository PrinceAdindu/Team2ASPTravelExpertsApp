using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class PackageController : Controller
    {
        private readonly TravelExpertsContext _context;

        public PackageController(TravelExpertsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var package = _context.Packages.ToList();
            return View(package);
        }
    }
}
