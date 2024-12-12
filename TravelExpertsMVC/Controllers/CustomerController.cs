using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

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

        // this method will be commented out when login functionality is implemented
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            customers = CustomerManager.GetCustomers();
            return View(customers);
        }


        public ActionResult Details(int id) /* <----this id should be replaced with customerid from session*/
        {
            Customer? customer = CustomerManager.GetCustomerById(id);
            return View(customer);
        }

        public ActionResult EditCustomer(int id) /* <----this id should be replaced with customerid from session*/
        {
            Customer? customer = CustomerManager.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult EditCustomer(int id, Customer customer) /* <----this id should be replaced with customerid from session*/
        {
            if (ModelState.IsValid)
            {
                CustomerManager.UpdateCustomer(id, customer);
                TempData["Message"] = "Customer Information Updated!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(customer);
            }
        }

        public ActionResult OrderHistory(int id) /* <----this id should be replaced with customerid from session*/
        {
            List<BookingsDTO> bookingHistory = new List<BookingsDTO>();
            bookingHistory = BookingManager.GetBookingSummary(id);
            return View(bookingHistory);
        }
    }
}


