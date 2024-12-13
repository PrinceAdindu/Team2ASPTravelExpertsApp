using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using TravelExpertsData;
using TravelExpertsData.DbManagers;
using TravelExpertsData.ViewModel;

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
		private TravelExpertsContext _context;
		private CustomerManager CustomerManager;
		public CustomerController() {
			this._context = new TravelExpertsContext();
			this.CustomerManager = new CustomerManager(_context);
		}
		 

		// this method will be commented out when login functionality is implemented
		public ActionResult Index()
		{
			List<Customer> customers = new List<Customer>();
			customers = CustomerManager.GetCustomers();
			return View(customers);
		}


		public ActionResult Details(int id) /* <----this id should be replaced with customerid from session*/
		{
			Customer? customer = CustomerManager.GetCustomerById(104);
			CustomerViewModel customerView = new CustomerViewModel()
			{
				CustFirstName = customer.CustFirstName,
				CustLastName = customer.CustLastName,
				CustHomePhone = customer.CustHomePhone,
				CustAddress = customer.CustAddress,
				CustBusPhone = customer.CustBusPhone!,
				CustCity = customer.CustCity,
				CustCountry = customer.CustCountry,
				CustEmail = customer.CustEmail,
				CustomerId = customer.CustomerId,
				CustPostal = customer.CustPostal,
				CustProv = customer.CustProv,
				AgentId = customer.AgentId,
				Agent = customer.Agent,
			};
			return View(customerView);
		}

		public ActionResult EditCustomer(int id) /* <----this id should be replaced with customerid from session*/
		{
			Customer? customer = CustomerManager.GetCustomerById(104);
			CustomerViewModel customerView = new CustomerViewModel()
			{
				CustFirstName = customer.CustFirstName,
				CustLastName = customer.CustLastName,
				CustHomePhone = customer.CustHomePhone,
				CustAddress = customer.CustAddress,
				CustBusPhone = customer.CustBusPhone!,
				CustCity = customer.CustCity,
				CustCountry = customer.CustCountry,
				CustEmail = customer.CustEmail,
				CustomerId = customer.CustomerId,
				CustPostal = customer.CustPostal,
				CustProv = customer.CustProv,
				AgentId = customer.AgentId,
				Agent= customer.Agent,
			};
			return View(customerView);
		}

		[HttpPost]
		public ActionResult EditCustomer(CustomerViewModel customerViewModel) /* <----this id should be replaced with customerid from session*/
		{
			if (ModelState.IsValid)
			{
				Customer customer = new Customer()
				{
					CustFirstName = customerViewModel.CustFirstName,
					CustLastName = customerViewModel.CustLastName,
					CustHomePhone = customerViewModel.CustHomePhone,
					CustAddress = customerViewModel.CustAddress,
					CustBusPhone = customerViewModel.CustBusPhone!,
					CustCity = customerViewModel.CustCity,
					CustCountry = customerViewModel.CustCountry,
					CustEmail = customerViewModel.CustEmail,
					CustomerId = customerViewModel.CustomerId,
					CustPostal = customerViewModel.CustPostal,
					CustProv = customerViewModel.CustProv,
					AgentId = customerViewModel.AgentId,
					Agent = customerViewModel.Agent,
				};
				CustomerManager.UpdateCustomer(customerViewModel.CustomerId, customer);
				TempData["Message"] = "Customer Information Updated!";
				return RedirectToAction("Index");
			}
			else
			{
				return View(customerViewModel);
			}
		}

		public ActionResult OrderHistory(int id) /* <----this id should be replaced with customerid from session*/
		{
			List<BookingsDTO> bookingHistory = new List<BookingsDTO>();
			bookingHistory = BookingManager.GetBookingSummary(id);
			return View(bookingHistory);
		}

		public ActionResult AddBalance()
		{
			Customer customer = CustomerManager.GetCustomerById(104);

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
			Customer customer = CustomerManager.GetCustomerById(104);
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
