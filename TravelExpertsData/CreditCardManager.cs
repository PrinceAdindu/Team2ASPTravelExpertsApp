using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class CreditCardManager
    {
        public static List<CreditCard> GetCreditCardsByCustomer(int id)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                return db.CreditCards
                    .Where(c => c.CustomerId == id)
                    .ToList();
            }
        }

        public static void AddCreditCard(CreditCard newCard)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                db.CreditCards.Add(newCard);
                db.SaveChanges();
            }
        }
    }
}
