using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelExpertsData;
using System.Threading.Tasks;

namespace TravelExperts.Controllers
{
    public class AccountController : Controller
    {
        // Route: /Account/Login
        public IActionResult Login(string returnUrl = "")
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        // POST: AccountController/Login
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer customer) // data collected on the form
        {
            Customer cust = CustomerManager.Authenticate(customer.Username, customer.Password);
            if (cust == null) // failed authentication
            {
                //    ViewData["ErrorMessage"] = "The " + (string.IsNullOrEmpty(customer.Username) ? "username" : "password") + " is incorrect.";
                //    return View(); // stay on the login page

                if (CustomerManager.UsernameExists(customer.Username))
                {
                    ViewData["ErrorMessage"] = "Password is incorrect.";
                }
                else 
                {
                    ViewData["ErrorMessage"] = "Username does not exist.";
                }

                return View();

            }
            // usr != null   - authentication passed
            if (cust.CustomerId != null)
            {
                HttpContext.Session.SetInt32("CurrentCustomer", (int)cust.CustomerId);
            }


            List<Claim> claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Name, cust.Username),
                new Claim(ClaimTypes.Name, cust.CustFirstName),
                new Claim(ClaimTypes.Name, cust.CustLastName),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme); // use cookies authentication
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal); // generates authentication cookie
            // if no return URL, go to the home page
            if (string.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
            {
                return RedirectToAction("MyBookings", "Bookings");
            }
            else
            {
                return Redirect(TempData["ReturnUrl"].ToString());
            }
        }


        // GET: AccountController/Logout
        public async Task<IActionResult> LogoutAsync()
        {
            // release authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // remove  current owner from the session
            HttpContext.Session.Remove("CurrentCustomer");

            return RedirectToAction("Index", "Home"); // go to the home page
        }

        // GET: AccountController/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
