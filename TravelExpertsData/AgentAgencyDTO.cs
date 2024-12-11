using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class AgentAgencyDTO
    {
        [StringLength(20)]
        public string? AgtFirstName { get; set; }

        [StringLength(5)]
        public string? AgtMiddleInitial { get; set; }

        [StringLength(20)]
        public string? AgtLastName { get; set; }

        [StringLength(20)]
        public string? AgtBusPhone { get; set; }

        [StringLength(50)]
        public string? AgtEmail { get; set; }

        [StringLength(20)]
        public string? AgtPosition { get; set; }
        [StringLength(50)]
        public string? AgncyAddress { get; set; }

        [StringLength(50)]
        public string? AgncyCity { get; set; }

        [StringLength(50)]
        public string? AgncyProv { get; set; }

        [StringLength(50)]
        public string? AgncyPostal { get; set; }

        [StringLength(50)]
        public string? AgncyCountry { get; set; }
    }
}
