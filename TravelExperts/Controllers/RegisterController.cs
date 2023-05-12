using System;
using TravelExpertsData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TravelExperts.Controllers
{
    public class RegisterController : Controller
    {
        private TravelExpertsContext context;
        public RegisterController(TravelExpertsContext ctx) => context = ctx;

        // GET: RegisterController
        [HttpGet]
        public IActionResult Index1() => View();

        // POST: RegisterController
        [HttpPost]
        public IActionResult Index1(Customer customer)
        {

            if (ModelState.IsValid)
            {

                if (context.Customers.Any(c => c.Username == customer.Username))
                {
                    ModelState.AddModelError("Username", "The username already exists. Please choose a different one.");
                    return View(customer);
                }

                if (context.Customers.Any(c => c.CustHomePhone == customer.CustHomePhone))
                {
                    ModelState.AddModelError("CustHomePhone", "The phone number already exists. Please choose a different one.");
                    return View(customer);
                }

                if (context.Customers.Any(c => c.CustBusPhone == customer.CustBusPhone))
                {
                    ModelState.AddModelError("CustBusPhone", "The phone number already exists. Please choose a different one.");
                    return View(customer);
                }

                if (context.Customers.Any(c => c.CustEmail == customer.CustEmail))
                {
                    ModelState.AddModelError("CustEmail", "The email address already exists. Please choose a different one.");
                    return View(customer);
                }



                var cust = new Customer
                {
                    Username = customer.Username,
                    Password = customer.Password,
                    CustFirstName = customer.CustFirstName,
                    CustLastName = customer.CustLastName,
                    CustHomePhone = customer.CustHomePhone,
                    CustCity = customer.CustCity,
                    CustAddress = customer.CustAddress,
                    CustCountry = customer.CustCountry,
                    CustEmail = customer.CustEmail,
                    CustPostal = customer.CustPostal,
                    CustProv = customer.CustProv,
                    //AgentId = customer.AgentId,
                    CustBusPhone = customer.CustBusPhone,
                    //CustomerId = customer.CustomerId,
                };
                // Add a record to the 
                context.Customers.Add(cust);
                context.SaveChanges();

                // Redirect to 
                return RedirectToAction("Welcome");
            }
            else
            {
                

                return View(customer);
            }


        }

        // POST: RegisterController/Welcome/5
        [HttpGet]
        public IActionResult Welcome(Customer customer)
        {
            return View();
        }
    }
}
