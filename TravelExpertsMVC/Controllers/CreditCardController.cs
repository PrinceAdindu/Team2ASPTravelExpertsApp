using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    [Authorize]
    public class CreditCardController : Controller
    {

		private CustomerManager CustomerManager;
        private readonly UserManager<User> _userManager;

        public CreditCardController(UserManager<User> userManager)
		{
			this.CustomerManager = new CustomerManager(); 
            this._userManager = userManager;


        }
        // GET: CreditCardController

        public async Task<ActionResult> AddCard()
        {
            SelectList list = CreateCardTypeList();
            ViewBag.CardTypes = list;

            var user = await _userManager.GetUserAsync(User);

            Customer customer = CustomerManager.GetCustomerById(user.CustomerId ?? 0);

            CreditCard newCard = new CreditCard();
            newCard.CustomerId = customer.CustomerId;
            return View(newCard);
        }

        [HttpPost]
        public ActionResult AddCard(CreditCard newCard)
        {
            SelectList list = CreateCardTypeList();
            ViewBag.CardTypes = list;
            ModelState.Remove("CustomerId");
            if (ModelState.IsValid)
            {
                CreditCardManager.AddCreditCard(newCard);
                return RedirectToAction("AddBalance", "Customer");
            }
            else
            {
                return View(newCard);
            }
        }

        private static SelectList CreateCardTypeList()
        {
            List<KeyValuePair<string, string>> cardTypes = new()
            {
                new KeyValuePair<string, string>("American Express", "AMEX"),
                new KeyValuePair<string, string>("Diner's Club", "Diners"),
                new KeyValuePair<string, string>("Mastercard", "MC"),
                new KeyValuePair<string, string>("VISA", "VISA")
            };
            var list = new SelectList(cardTypes, "Value", "Key");
            return list;
        }
    }
}
