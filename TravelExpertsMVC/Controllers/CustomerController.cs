using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class CustomerController : Controller
    {
        // this method will be commented out when login functionality is implemented
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            customers = CustomerManager.GetCustomers();
            return View(customers);
        }


        public ActionResult Details(int id)
        {
            Customer? customer = CustomerManager.GetCustomerById(id);
            return View(customer);
        }

        public ActionResult EditCustomer(int id)
        {
            Customer? customer = CustomerManager.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult EditCustomer(int id, Customer customer)
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

        public ActionResult OrderHistory(int id)
        {
            List<BookingsDTO> bookingHistory = new List<BookingsDTO>();
            bookingHistory = BookingManager.GetBookingSummary(id);
            return View(bookingHistory);
        }
    }
}


