using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerController
        public ActionResult AddBalance()
        {
            Customer customer = CustomerManager.GetCustomerByID(104);

            int id = customer.CustomerId;

            List<CreditCard> creditCards = CreditCardManager.GetCreditCardsByCustomer(id);
            if (creditCards != null)
            {
                var list = new SelectList(creditCards, "CreditCardId", "CCNumber").ToList();
                TempData["Error"] = "You do not have any saved credit cards, please add a card before continuing.";
                ViewBag.Cards = list;
            }
            else
            {
                var list = new SelectList("").ToList();
                ViewBag.Cards = list;
            }

            return View(customer);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
