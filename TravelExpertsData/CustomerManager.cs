using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class CustomerManager
    {
        public static Customer GetCustomerByID(int custID)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                return db.Customers.Find(custID);
            }
        }

        public static void UpdateBalance(int custID, decimal updatedBalance)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                Customer customer = db.Customers.Find(custID);
                if (customer != null)
                {
                    customer.Balance = updatedBalance;
                    db.SaveChanges();
                }
            }
        }
    }
}
