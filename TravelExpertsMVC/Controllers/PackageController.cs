using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class PackageController : Controller
    {
        // GET: PackageController
        public ActionResult Index()
        {
            List<Package> packages = PackageManager.GetPackages();
            return View(packages);
        }

        // GET: PackageController/Details/5
        public ActionResult Payment(int id)
        {
            Package pkg = PackageManager.GetPackageByID(id)!;
            Customer user = CustomerManager.GetCustomerByID(104);

            decimal paymentTotal = (decimal)(pkg.PkgBasePrice + pkg.PkgAgencyCommission)!;
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
                return RedirectToAction("Index");
            }
        }

        // GET: PackageController/Create
        [HttpPost]
        public ActionResult Payment(int id, int paymentID = 0)
        {
            Package pkg = PackageManager.GetPackageByID(id);
            Customer user = CustomerManager.GetCustomerByID(104);

            decimal paymentTotal = (decimal)(pkg.PkgBasePrice + pkg.PkgAgencyCommission)!;
            decimal custBalance = user.Balance;

            decimal finalBalance = custBalance - paymentTotal;

            CustomerManager.UpdateBalance(user.CustomerId, finalBalance);

            return RedirectToAction("ThankYou");
        }

        // GET: PackageController/Edit/5
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}
