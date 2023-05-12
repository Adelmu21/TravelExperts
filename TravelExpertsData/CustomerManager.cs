using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class CustomerManager
    {


        /// <summary>
        /// User is authenticated based on credentials and a user returned if exists or null if not.
        /// </summary>
        /// <param name="username">Username as string</param>
        /// <param name="password">Password as string</param>
        /// <returns>A user object or null.</returns>
        /// <remarks>
        /// Add additional for the docs for this application--for developers.
        /// </remarks>
        public static Customer Authenticate(string username, string password)
        {
            Customer? customer = null;
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                customer = db.Customers.SingleOrDefault(cust => cust.Username == username
                                                             && cust.Password == password);
                if(username == null)
                {
                    return null;
                }
                if (password == null)
                {
                    return null;
                }
            }

            return customer; //this will either be null or an object
        }


        public static Customer FindCustomer(string email, TravelExpertsContext context)
        {
            return context.Customers.SingleOrDefault(c => c.CustEmail == email);
        }


        //public static bool UsernameExists(string username)
        //{
        //    using (var db = new TravelExpertsContext())
        //    {
        //        var customer = db.Customers.FirstOrDefault(c => c.Username == username);
        //        return (customer != null);
        //    }
        //}

        public static bool UsernameExists(string username)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                return db.Customers.Any(c => c.Username == username);
            }
        }



    }
}
