using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.DbManagers
{
    public class CustomerManager
    {
		private readonly TravelExpertsContext _context;
		public CustomerManager(TravelExpertsContext context)
		{
			_context = context;
		}
		public void CreateCustomer (Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

		public async Task<byte[]?> GetProfileImageByIdAsync(int userId)
		{
			if (string.IsNullOrEmpty(Convert.ToString(userId))) return null;
			return await _context.Customers
				.Where(c => c.CustomerId == userId) // Assuming there's a UserId column in Customer table
				.Select(c => c.ProfileImage) // Selecting the ProfileImage column
				.FirstOrDefaultAsync();
		}
	}
}
