using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData
{
    public class BookingManager
    {

        public static List<Booking> GetPackagesByCustomer(int id = 0)
        {
            List<Booking> bookings = null;
            TravelExpertsContext db = new TravelExpertsContext();
            if (id == 0) // no filtering
            {

                bookings = db.Bookings.Include(l => l.Customer).Include(l => l.Package).ToList();
            }
            else // filter by owner
            {
                bookings = db.Bookings.
                Where(l => l.CustomerId == id).
                Include(l => l.Customer).Include(l => l.Package).ToList();


            }
            return bookings;
        }
    }
}
