using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class CreditCardController : Controller
    {

		private TravelExpertsContext _context;
		private CustomerManager CustomerManager;
		public CreditCardController()
		{
			this._context = new TravelExpertsContext();
			this.CustomerManager = new CustomerManager(_context);
		}
		// GET: CreditCardController
		public ActionResult AddCard()
        {
            SelectList list = CreateCardTypeList();
            ViewBag.CardTypes = list;

            Customer customer = CustomerManager.GetCustomerById(104);

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
