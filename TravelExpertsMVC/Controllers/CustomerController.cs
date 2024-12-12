using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult AddBalance()
        {
            Customer customer = CustomerManager.GetCustomerByID(104);

            int id = customer.CustomerId;

            List<CreditCard> creditCards = CreditCardManager.GetCreditCardsByCustomer(id);
            if (creditCards != null)
            {
                var list = new SelectList(creditCards, "CreditCardId", "Ccnumber").ToList();
                ViewBag.Cards = list;
            }
            else
            {
                var list = new SelectList("").ToList();
                TempData["NoCards"] = "You do not have any saved credit cards, please add a card before continuing.";
                ViewBag.Cards = list;
            }

            CustomerDTO customerAddBalance = new CustomerDTO()
            {
                CurrentBalance = customer.Balance,
                AddToBalance = 0,
                CustCard = ""
            };

            return View(customerAddBalance);
        }

        [HttpPost]
        public ActionResult AddBalance(CustomerDTO customerAddBalance)
        {
            Customer customer = CustomerManager.GetCustomerByID(104);
            List<CreditCard> creditCards = CreditCardManager.GetCreditCardsByCustomer(customer.CustomerId);
            var list = new SelectList(creditCards, "CreditCardId", "Ccnumber").ToList();
            ViewBag.Cards = list;

            if (ModelState.IsValid)
            {
                decimal updatedBalance = customerAddBalance.CurrentBalance + customerAddBalance.AddToBalance;
                CustomerManager.UpdateBalance(customer.CustomerId, updatedBalance);

                return RedirectToAction("Index", "Package");
            }
            else
            {
                return View(customerAddBalance);
            }
        }
    }
}
