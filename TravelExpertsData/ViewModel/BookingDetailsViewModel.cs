using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.ViewModel
{
    public class BookingDetailsViewModel
    {
        public int? CustomerId { get; set; } = null!;
        public int PackageId { get; set; }
        public string PkgName { get; set; }
        public string PkgDesc { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? PkgStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? PkgEndDate { get; set; }
        public decimal PkgBasePrice { get; set; }
        public string? PackageImage { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Number of travellers must be between 1 and 20.")]
        public int NumberOfTravellers { get; set; }

        [Required]
        public string? TripTypeId { get; set; }

        [Required]
        public string? ClassId { get; set; }

        [Required]
        public string Destination { get; set; }

        public IEnumerable<TripType>? TripTypes { get; set; }
        public IEnumerable<Class>? Classes { get; set; }

        public decimal AgencyCommission { get; set; }

        public int? pProductSupplierId { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public string BookingNo { get; set; }
    }

}
