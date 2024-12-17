using Microsoft.AspNetCore.Mvc;
using TravelExpertsData;
using System.Linq;
using TravelExpertsData.ViewModel;
using TravelExpertsData.DbManagers;
using Microsoft.AspNetCore.Authorization;

namespace TravelExperts.Controllers
{
    public class BookingController : Controller
    {
        private readonly TravelExpertsContext _context;
        private readonly BookingManager _bookingManager;
        public BookingController(TravelExpertsContext context)
        {
            _context = context;
            _bookingManager = new BookingManager(context);
        }

        // GET: Booking/Details/{id}
        [Authorize]
        public IActionResult Details(int id)
        {
            var package = _context.Packages.FirstOrDefault(p => p.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            var tripTypes = _context.TripTypes.ToList();
            var classes = _context.Classes.ToList();

            var viewModel = new BookingDetailsViewModel
            {
                PackageId = package.PackageId,
                PkgName = package.PkgName,
                PkgDesc = package.PkgDesc!,
                PkgStartDate = package.PkgStartDate!,
                PkgEndDate = package.PkgEndDate!,
                PkgBasePrice = package.PkgBasePrice,
                PackageImage = "/images/" + package.PackageId + ".jpg", // This selects the appropriate image for each package
                TripTypes = tripTypes,
                Classes = classes,
                AgencyCommission = (decimal)package.PkgAgencyCommission!,
            };

            return View(viewModel);
        }

        // POST: Booking/Submit
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Submit(BookingDetailsViewModel model)
        {
            model.TripTypes = _context.TripTypes.ToList();
            model.Classes = _context.Classes.ToList();
            model.PackageImage = "/images/package-1";
            ModelState.Remove("BookingNo");
            if (ModelState.IsValid)
            {
                var booking = new Booking()
                {
                    BookingDate = model.BookingDate,
                    BookingNo = GenerateBookingNumber(model.BookingDate.ToString()),
                    TravelerCount = model.NumberOfTravellers,
                    CustomerId = model.CustomerId,
                    TripTypeId = model.TripTypeId,
                    PackageId = model.PackageId,
                };
                await _bookingManager.AddBookingAsync(booking);

                var bookingDetail = new BookingDetail
                {
                    BookingId = booking.BookingId,
                    ItineraryNo = 144,
                    Description = model.PkgDesc,
                    Destination = model.Destination,
                    BasePrice = model.PkgBasePrice*model.NumberOfTravellers,
                    ClassId = model.ClassId,
                    TripStart = model.PkgStartDate ?? DateTime.Now,
                    TripEnd = model.PkgEndDate ?? DateTime.Now,
                    AgencyCommission = model.AgencyCommission,
                    ProductSupplierId = 44
                };

                await _bookingManager.AddBookingDetailsAsync(bookingDetail);

                return RedirectToAction("Payment", "Package", new { id = model.PackageId });
            }
            else
            {
                ModelState.AddModelError("", "Customer Information not found");
            }

            return View("Details", model);
        }

        public string GenerateBookingNumber(string BookingDate)
        {
            return "Booking- "+ BookingDate;
        }
    }
}
