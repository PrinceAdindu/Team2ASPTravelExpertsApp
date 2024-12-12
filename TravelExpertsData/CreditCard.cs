using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData;

[Index("CustomerId", Name = "CustomersCreditCards")]
public partial class CreditCard
{
    [Key]
    public int CreditCardId { get; set; }

    [Column("CCName")]
    [StringLength(10)]
    [Display(Name = "Card Type")]
    public string Ccname { get; set; } = null!;

    [Column("CCNumber")]
    [StringLength(50)]
    [Display(Name = "Card Number")]
    [RegularExpression("^([0-9]{4})([0-9]{4})([0-9]{4})([0-9]{4})$", ErrorMessage = "Ensure your card number is entered in the format 1111222233334444.")]
    public string Ccnumber { get; set; } = null!;

    [Column("CCExpiry", TypeName = "datetime")]
    [Display(Name = "Expiry Date")]
    [Range(typeof(DateTime), "2024-12-01", "2034-12-01", ErrorMessage = "Please ensure your card is not already expired.")]
    public DateTime Ccexpiry { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CreditCards")]
    public virtual Customer? Customer { get; set; } = null!;
}
