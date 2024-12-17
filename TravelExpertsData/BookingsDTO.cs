using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class BookingsDTO
    {
        [Display(Name ="Booking Description")]
        public string BookingDescription { get; set; }

        [Display(Name = "Destination")]
        public string BookingDestination { get; set; }

        [Display(Name = "Booking #")]
        public string BookingNo { get; set; }

        [Display(Name = "Trip Type")]
        public string TripTypeName { get; set; }

        [Display(Name = "Flight Class")]
        public string ClassName { get; set; }

        //[Display(Name = "Region")]
        //public string? RegionName { get; set; }

        //[Display(Name = "Fee Amount")]
        //public decimal? FeeAmount { get; set; }

        //[Display(Name = "Fee Details")]
        //public string? FeeName { get; set; }

        [Display(Name ="Base Price")]
        public decimal? BasePrice { get; set; }

        [Display(Name = "Agency Commission")]
        public decimal? AgencyCommission { get; set; }
    }
}
