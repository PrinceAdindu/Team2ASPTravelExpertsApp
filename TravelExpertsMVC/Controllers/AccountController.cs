using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExpertsData;
using TravelExpertsData.ViewModel;
using TravelExpertsData.DbManagers;
using Microsoft.AspNetCore.Authorization;

namespace TravelExpertsMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private CustomerManager _customerManager;
        private TravelExpertsContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, TravelExpertsContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _customerManager = new CustomerManager();
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home"); // Redirect to Home on success
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "User account locked out.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerViewModel.CustEmail,
                    FullName = registerViewModel.CustFirstName + " " + registerViewModel.CustLastName,
                    Email = registerViewModel.CustEmail,
                    PhoneNumber = registerViewModel.CustHomePhone
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password!);

                byte[]? imageBytes = null;

                if (registerViewModel.ProfileImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await registerViewModel.ProfileImage.CopyToAsync(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }
                }

                if (result.Succeeded)//If successfull, signin the user.
                {
                    var customer = new Customer
                    {
                        CustFirstName = registerViewModel.CustFirstName!,
                        CustLastName = registerViewModel.CustLastName!,
                        CustAddress = registerViewModel.CustAddress,
                        CustCity = registerViewModel.CustCity,
                        CustProv = registerViewModel.CustProv,
                        CustPostal = registerViewModel.CustPostal,
                        CustCountry = registerViewModel.CustCountry,
                        CustHomePhone = registerViewModel.CustHomePhone,
                        CustBusPhone = registerViewModel.CustBusPhone,
                        CustEmail = registerViewModel.CustEmail,
                        AgentId = registerViewModel.AgentId,
                        ProfileImage = imageBytes
                    };

                    _customerManager.CreateCustomer(_context, customer);
                    await _context.SaveChangesAsync();

                    user.CustomerId = customer.CustomerId;
                    await _userManager.UpdateAsync(user);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)//otherwise display errors.
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            return View();
        }
    }
}
