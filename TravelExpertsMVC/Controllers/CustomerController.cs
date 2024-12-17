using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TravelExpertsData;
using TravelExpertsData.DbManagers;
using TravelExpertsData.ViewModel;

namespace TravelExpertsMVC.Controllers
{
    [Authorize]
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

		private TravelExpertsData.CustomerManager CustomerManager;
		private readonly TravelExpertsContext _context;
        private readonly UserManager<User> _userManager;

        public CustomerController(TravelExpertsContext ctx, UserManager<User> userManager) {
			_userManager = userManager;
			this._context = ctx;
            this.CustomerManager = new TravelExpertsData.CustomerManager();
		}
		 

		// this method will be commented out when login functionality is implemented
		//public ActionResult Index()
		//{
		//	List<Customer> customers = new List<Customer>();
		//	customers = CustomerManager.GetCustomers();
		//	return View(customers);
		//}


		public async Task<ActionResult> Details(int id) /* <----this id should be replaced with customerid from session*/
		{
			Customer? customer = CustomerManager.GetCustomerById(id);
			CustomerViewModel customerView = new CustomerViewModel()
			{
				CustFirstName = customer.CustFirstName,
				CustLastName = customer.CustLastName,
				CustHomePhone = customer.CustHomePhone,
				CustAddress = customer.CustAddress,
				CustBusPhone = customer.CustBusPhone ?? "",
				CustCity = customer.CustCity,
				CustCountry = customer.CustCountry,
				CustEmail = customer.CustEmail,
				CustomerId = customer.CustomerId,
				CustPostal = customer.CustPostal,
				CustProv = customer.CustProv,
				AgentId = customer.AgentId ?? 0,
				ProfileImage = customer.ProfileImage
			};
			return View(customerView);
		}

		[HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile ProfileImage)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ProfileImage == null || ProfileImage.Length == 0)
			{
				TempData["Error"] = "Please select a valid image file.";			
                return RedirectToAction("Details", new { id = user.CustomerId });
            }

            try
            {
                // Read the file into a byte array
                using var memoryStream = new MemoryStream();
                await ProfileImage.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                // Find the customer in the database
                var customer = await _context.Customers.FindAsync(user.CustomerId);
                if (customer == null)
                {
                    TempData["Error"] = "Customer not found.";
                    return RedirectToAction("Index");
                }

                // Update the profile image
                customer.ProfileImage = imageBytes;

                // Save changes to the database
                await _context.SaveChangesAsync();

                TempData["Success"] = "Profile image updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while uploading the image: " + ex.Message;
            }

            return RedirectToAction("Details", new { id = user.CustomerId });
        }


        public ActionResult EditCustomer(int id) /* <----this id should be replaced with customerid from session*/
		{
			Customer? customer = CustomerManager.GetCustomerById(id);
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

		public async Task<ActionResult> AddBalance()
		{
            var currentUser = await _userManager.GetUserAsync(User);
            Customer customer = CustomerManager.GetCustomerById(currentUser.CustomerId ?? 104);

			int id = currentUser.CustomerId ?? 0;

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
		public async Task<ActionResult> AddBalance(CustomerDTO customerAddBalance)
		{
            var currentUser = await _userManager.GetUserAsync(User);
            Customer customer = CustomerManager.GetCustomerById(currentUser.CustomerId ?? 104);
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
