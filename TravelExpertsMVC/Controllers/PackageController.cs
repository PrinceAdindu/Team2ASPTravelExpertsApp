using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using TravelExpertsData;
using TravelExpertsData.DbManagers;

namespace TravelExpertsMVC.Controllers
{
    public class PackageController : Controller
    {
		private TravelExpertsContext _context;
		private TravelExpertsData.CustomerManager CustomerManager;
        private readonly UserManager<User> _userManager;

        public PackageController(UserManager<User> userManager)
		{
			this._context = new TravelExpertsContext();
            this.CustomerManager = new TravelExpertsData.CustomerManager();
            _userManager = userManager;
        }
        // GET: PackageController
        //public ActionResult Index()
        //      {
        //          List<Package> packages = PackageManager.GetPackages();
        //          return View(packages);
        //      }

        public IActionResult Index(bool flag = true)
        {
            List<Package> packages = PackageManager.GetPackages();
            if (flag)
            {
                ViewBag.ErrorMessage = TempData["Error"];
                return View(packages);
            }
            return View(packages);
        }

        // GET: PackageController/Details/5
        [Authorize]
        public async Task<ActionResult> Payment(int id)
        {
            Package pkg = PackageManager.GetPackageByID(id)!;
            int bookingId = (int)TempData["bookingId"];
            Booking booking = BookingManager.GetBooking(bookingId); 

            var currentUser = await _userManager.GetUserAsync(User);
            int customerId = currentUser?.CustomerId ?? 0;
            Customer user = CustomerManager.GetCustomerById(customerId);

            decimal paymentTotal = (decimal)((pkg.PkgBasePrice * (decimal)booking.TravelerCount) + pkg.PkgAgencyCommission)!;
            decimal custBalance = user.Balance;

            ViewBag.PaymentTotal = paymentTotal.ToString("c");
            ViewBag.Balance = custBalance.ToString("c");

            if (custBalance >= paymentTotal)
            {
                TempData["Error"] = null;
                return View(pkg);
            }
            else
            {
                TempData["Error"] = $"You have insufficient funds, your current balance is ${custBalance}.";
                return RedirectToAction("Index",true);
            }
        }

        // GET: PackageController/Create
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Payment(int id, int paymentID = 0)
        {
            Package pkg = PackageManager.GetPackageByID(id)!;
            var currentUser = await _userManager.GetUserAsync(User);
            int customerId = currentUser?.CustomerId ?? 0;
            Customer user = CustomerManager.GetCustomerById(customerId);

            decimal paymentTotal = (decimal)(pkg.PkgBasePrice + pkg.PkgAgencyCommission)!;
            decimal custBalance = user.Balance;

            decimal finalBalance = custBalance - paymentTotal;

            CustomerManager.UpdateBalance(user.CustomerId, finalBalance);

            return RedirectToAction("ThankYou");
        }

        // GET: PackageController/Edit/5
        [Authorize]
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}
