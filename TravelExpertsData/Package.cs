using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData;

public partial class Package
{
    [Key]
    public int PackageId { get; set; }


    [Display(Name ="Package Name")]
    [StringLength(50)]
    public string PkgName { get; set; } = null!;

    [Display(Name = "Start Date")]
    [Column(TypeName = "datetime")]
    public DateTime? PkgStartDate { get; set; }

    [Display(Name = "End Date")]
    [Column(TypeName = "datetime")]
    public DateTime? PkgEndDate { get; set; }

    [Display(Name = "Package Description")]

    [StringLength(50)]
    public string? PkgDesc { get; set; }

    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal PkgBasePrice { get; set; }

    [Display(Name = "Agency Commission")]

    [Column(TypeName = "money")]
    public decimal? PkgAgencyCommission { get; set; }

    [InverseProperty("Package")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Package")]
    public virtual ICollection<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; } = new List<PackagesProductsSupplier>();
}
