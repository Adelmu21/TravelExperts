using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExpertsData;
using TravelExperts.Models;
using Microsoft.Data.SqlClient;

namespace TravelExperts.Controllers
{
    //[Authorize]
    public class BookingsController : Controller
    {
        private readonly TravelExpertsContext _context;

        public BookingsController(TravelExpertsContext context)
        {
            _context = context;
        }

        // GET: BookingsController
        [Authorize()]
        public IActionResult Index()
        {

            // Variable needed to show the results
            var ViewModel = _context.Packages.Select(p => new PackagesViewModel
            {
                PackageId = p.PackageId,
                PkgName = p.PkgName,
                PkgDesc = p.PkgDesc,
                PkgStartDate = p.PkgStartDate,
                PkgEndDate = p.PkgEndDate,
                PkgBasePrice = p.PkgBasePrice,
                PkgAgencyCommission = p.PkgAgencyCommission,
            }).ToList();

            return View(ViewModel);
        }

        // POST: BookingsController/BookPackage
        [HttpPost]
        public async Task<IActionResult> BookPackage(int packageId)
        {
            // Get the current customer ID
            int? customerId = HttpContext.Session.GetInt32("CurrentCustomer");
            if (customerId != null)
            {

                // Check that the package ID exists in the Packages table
                var package = await _context.Packages.FindAsync(packageId);
                if (package == null)
                {
                    // Return an error message or redirect to an error page
                    return RedirectToAction("Error", "Home");
                }
                else
                {

                    _context.Bookings.Add(new Booking { PackageId = packageId, CustomerId = (int)customerId });
                    _context.SaveChanges();
                }
            }

            // Redirect to the MyBookings page
            return RedirectToAction("MyBookings", "Bookings");
        }


        // POST: BookingsController/MyBookings/5
        [Authorize()]
        public ActionResult MyBookings()
        {
            int? customerId = HttpContext.Session.GetInt32("CurrentCustomer");
            if (customerId != null)
            {
                // Retrieve the bookings for the current customer from the database
                var bookings = _context.Bookings.Include(b => b.Package).Where(b => b.CustomerId == customerId).ToList();

                // Pass the bookings data to the Razor view
                return View(bookings);
            }
            return View();
        }

        // NOT BEING USED


        //[Authorize()]
        //public ActionResult PastBookings()
        //{
        //    List<Package> package = null;
        //    int? customerId = HttpContext.Session.GetInt32("CurrentCustomer");
        //    if (customerId == null)
        //    {
        //        customerId = 0;
        //    }

        //    var bookingDetails = _context.BookingDetails
        //         .Include(bd => bd.Booking)
        //            .ThenInclude(b => b.Customer)
        //                .ThenInclude(c => c.Bookings)
        //                    .ThenInclude(b => b.Package)
        //        .Where(bd => bd.Booking.CustomerId == customerId)
        //        .Select(bd => new BookingsViewModel
        //        {
        //            BookingId = bd.BookingId,
        //            Booking = bd.Booking,
        //            Package = bd.Booking.Package,
        //            TripStart = bd.TripStart,
        //            TripEnd = bd.TripEnd,
        //            Description = bd.Description,
        //            Destination = bd.Destination,
        //            BasePrice = bd.BasePrice,
        //            AgencyCommission = bd.AgencyCommission,
        //            CustFirstName = bd.Booking.Customer.CustFirstName,
        //            CustLastName = bd.Booking.Customer.CustLastName
        //        })
        //        .ToList();


        //    return View(bookingDetails);


        //}



        // POST: BookingsController/UpdateBooking
        [HttpPost]
        public IActionResult UpdateBooking(int bookingId, int travelerCount)
        {
            // Find the booking by ID
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);

            if (booking == null)
            {
                // If booking is not found, return an error message or redirect to an error page
                return RedirectToAction("Error", "Home");
            }

            // Update the traveler count
            booking.TravelerCount = travelerCount;

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to the MyBookings page
            return RedirectToAction("MyBookings", "Bookings");
        }


        // POST: BookingsController/RemoveBooking
        [HttpPost]
        public async Task<IActionResult> RemoveBooking(int bookingId)
        {
            // Get the current customer ID
            int? customerId = HttpContext.Session.GetInt32("CurrentCustomer");
            if (customerId != null)
            {
                try
                {
                    // Check that the booking ID exists in the Bookings table
                    var booking = await _context.Bookings.FindAsync(bookingId);
                    if (booking == null || booking.CustomerId != customerId)
                    {
                        // Return an error message or redirect to an error page
                        return RedirectToAction("Error", "Home");
                    }
                    else
                    {
                        // Update the foreign key column in the BookingDetails table to set it to NULL
                        foreach (var bookingDetail in booking.BookingDetails)
                        {
                            bookingDetail.BookingId = null;
                        }

                        // Delete the record in the Bookings table
                        _context.Bookings.Remove(booking);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateException ex)
                {
                    // Handle the specific error that occurred
                    if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                    {
                        // This is a foreign key constraint violation error
                        // Handle it by displaying an error message to the user
                        TempData["ErrorMessage"] = "This booking cannot be removed because it has associated booking details.";
                    }
                    else
                    {
                        // Handle other types of errors by displaying a generic error message
                        TempData["ErrorMessage"] = "An error occurred while removing the booking. Please try again later.";
                    }
                }
            }

            // Redirect to the MyBookings page
            return RedirectToAction("MyBookings", "Bookings");
        }

        // POST: BookingsController/CalculateTotalPrice/5
        [HttpPost]
        public IActionResult CalculateTotalPrice(int packageId, int travelerCount)
        {
            var package = _context.Packages.Find(packageId);

            if (package == null)
            {
                return BadRequest("Invalid package ID.");
            }

            var totalPrice = package.PkgBasePrice * travelerCount;

            return Ok(totalPrice);
        }
    }
}
