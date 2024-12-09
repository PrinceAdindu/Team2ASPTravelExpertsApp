using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class User : IdentityUser
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int Id {  get; set; }
        public string FullName { get; set; }
    }
}
