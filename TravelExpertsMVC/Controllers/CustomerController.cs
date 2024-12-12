using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;
using TravelExpertsData.DbManagers;

namespace TravelExpertsMVC.Controllers
{
    public class CustomerController : Controller
    {
        /*
         * Instructions on how to merge my branch with the rest of the app
         * 
         * This Index method below should be removed.
         * The methods for Details, EditCustomer and OrderHistory
         * all require an id. This should be the customerId of  the customer who is logged in
         * Once this value has been provided the controllers and views should all function properly
         * and the Index method can be removed/commented out
         * 
         * Kazi
         */
        // GET: CustomerController
        public ActionResult AddBalance()
        {
            Customer customer = CustomerManager.GetCustomerByID(104);

        // this method will be commented out when login functionality is implemented
        public ActionResult Index()
            int id = customer.CustomerId;

            List<CreditCard> creditCards = CreditCardManager.GetCreditCardsByCustomer(id);
            if (creditCards != null)
            {
            List<Customer> customers = new List<Customer>();
            customers = CustomerManager.GetCustomers();
            return View(customers);
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

        public ActionResult Details(int id) /* <----this id should be replaced with customerid from session*/
        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            Customer? customer = CustomerManager.GetCustomerById(id);
            return View(customer);
            return View();
        }

        public ActionResult EditCustomer(int id) /* <----this id should be replaced with customerid from session*/
        // GET: CustomerController/Create
        public ActionResult Create()
        {
            Customer? customer = CustomerManager.GetCustomerById(id);
            return View(customer);
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        public ActionResult EditCustomer(int id, Customer customer) /* <----this id should be replaced with customerid from session*/
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            try
            {
                CustomerManager.UpdateCustomer(id, customer);
                TempData["Message"] = "Customer Information Updated!";
                return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }
            else
            catch
            {
                return View(customer);
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult OrderHistory(int id) /* <----this id should be replaced with customerid from session*/
        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
            List<BookingsDTO> bookingHistory = new List<BookingsDTO>();
            bookingHistory = BookingManager.GetBookingSummary(id);
            return View(bookingHistory);
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
