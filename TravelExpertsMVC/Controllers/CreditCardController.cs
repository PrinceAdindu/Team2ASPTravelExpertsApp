using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class CreditCardController : Controller
    {
        // GET: CreditCardController
        public ActionResult AddCard()
        {
            List<string> cardTypes = new()
            {
                "AMEX",
                "Diners",
                "MC",
                "VISA"
            };
            var list = new SelectList(cardTypes);
            ViewBag.CardTypes = list;

            CreditCard newCard = new CreditCard();
            return View(newCard);
        }

        // GET: CreditCardController/Details/5
        [HttpPost]
        public ActionResult AddCard(CreditCard newCard)
        {
            return View();
        }
    }
}
