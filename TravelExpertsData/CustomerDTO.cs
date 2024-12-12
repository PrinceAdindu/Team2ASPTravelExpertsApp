using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class CustomerDTO
    {
        [Required]
        [Display(Name = "Current Balance")]
        public decimal CurrentBalance { get; set; }

        [Required]
        [Display(Name = "Amount to Add")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please ensure the amount you are adding is at least $1.")]
        public decimal AddToBalance { get; set; }

        [Required]
        [Display(Name = "Credit Card")]
        public string CustCard {  get; set; }
    }
}
