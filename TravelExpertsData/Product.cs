using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData;

[Index("ProductId", Name = "ProductId")]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string ProdName { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<ProductsSupplier> ProductsSuppliers { get; set; } = new List<ProductsSupplier>();
}
