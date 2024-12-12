using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace TravelExpertsData;

[Index("AgentId", Name = "EmployeesCustomers")]
public partial class Customer
{
    [Key]

    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Please enter First Name")]
    [MaxLength(25, ErrorMessage = "First Name cannot be more than 25 characters")]
    [Display(Name = "First Name")]
    [StringLength(25)]
    public string CustFirstName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter Last Name")]
    [MaxLength(25, ErrorMessage = "Last Name cannot be more than 25 characters")]
    [Display(Name = "Last Name")]
    [StringLength(25)]
    public string CustLastName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter Address")]
    [MaxLength(75, ErrorMessage = "Address cannot be more than 75 characters")]
    [Display(Name = "Street Address")]

    [StringLength(75)]
    public string CustAddress { get; set; } = null!;

    [Required(ErrorMessage = "Please enter City")]
    [MaxLength(50, ErrorMessage = "City cannot be more than 25 characters")]
    [Display(Name = "City")]

    [StringLength(50)]
    public string CustCity { get; set; } = null!;


    [MaxLength(2, ErrorMessage = "Province cannot be more than 2 characters")]
    [Display(Name = "Province")]

    [StringLength(2)]
    public string CustProv { get; set; } = null!;


    [Required(ErrorMessage = "Please enter Postal Code")]
    [RegularExpression(@"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", 
        ErrorMessage = "Postal Code must be in format: A1A 1A1")]
    [Display(Name = "Postal Code")]
    [StringLength(7)]
    public string CustPostal { get; set; } = null!;


    [MaxLength(25, ErrorMessage = "Country cannot be more than 25 characters")]
    [Display(Name = "Country")]

    [StringLength(25)]
    public string? CustCountry { get; set; }


    [RegularExpression(@"^\d{10}$", 
        ErrorMessage = "Phone number must be exactly 10 digits and contain only numerical characters")]
    [Display(Name = "Home Phone Number")]

    [StringLength(20)]
    public string? CustHomePhone { get; set; }

    [Required(ErrorMessage = "Please enter Business Phone Number")]
    [RegularExpression(@"^\d{10}$", 
        ErrorMessage = "Phone number must be exactly 10 digits and contain only numerical characters")]
    [Display(Name = "Business Phone Number")]
    [StringLength(20)]
    public string CustBusPhone { get; set; } = null!;


    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage ="Email must be in the format: example@example.com")]
    [MaxLength(50, ErrorMessage = "Email cannot be more than 50 characters")]
    [Display(Name = "Email")]
    [StringLength(50)]
    public string CustEmail { get; set; } = null!;

    [Display(Name = "Assigned Agent")]
    public int? AgentId { get; set; }

    public decimal Balance { get; set; }

    [ForeignKey("AgentId")]
    [InverseProperty("Customers")]
    public virtual Agent? Agent { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Customer")]
    public virtual ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomersReward> CustomersRewards { get; set; } = new List<CustomersReward>();
}
