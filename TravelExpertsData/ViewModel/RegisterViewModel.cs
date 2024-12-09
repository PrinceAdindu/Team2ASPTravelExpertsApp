using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.ViewModel
{
    public class RegisterViewModel
    {
        [StringLength(25)]
        [Required]
        [Display (Name = "First Name")]
        public string CustFirstName { get; set; } = null!;

        [StringLength(25)]
        [Required]
        [Display(Name = "Last Name")]
        public string CustLastName { get; set; } = null!;

        [StringLength(75)]
        [Required]
        [Display(Name = "Address")]
        public string CustAddress { get; set; } = null!;

        [StringLength(50)]
        [Required]
        [Display(Name = "City")]
        public string CustCity { get; set; } = null!;

        [StringLength(2)]
        [Required]
        [Display(Name = "Province")]
        public string CustProv { get; set; } = null!;

        [StringLength(7)]
        [Required]
        [Display(Name = "Postal Code")]
        public string CustPostal { get; set; } = null!;

        [StringLength(25)]
        [Required]
        [Display(Name = "Country")]
        public string? CustCountry { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Home Phone")]
        public string? CustHomePhone { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Business Phone")]
        public string CustBusPhone { get; set; } = null!;

        [StringLength(50)]
        [Required]
        [Display(Name = "Email")]
        public string CustEmail { get; set; } = null!;

        [Required]
        [Display(Name = "Agent")]
        public int? AgentId { get; set; }

        [StringLength(26)]
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(26)]
        [Required]
        [Display(Name = "Confirm Pasword")]
        public string ConfirmPassword { get; set; }
    }
}
