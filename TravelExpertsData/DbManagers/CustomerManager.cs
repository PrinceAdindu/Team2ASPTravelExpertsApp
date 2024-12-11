using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.DbManagers
{
    public class CustomerManager
    {
        public void CreateCustomer (TravelExpertsContext _context, Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
}
