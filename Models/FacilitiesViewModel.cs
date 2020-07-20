using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class FacilitiesViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Facility Name")]
        public String FacilityName { get; set; }
        [Display(Name = "Owner Company")]
        public int CompanyId { get; set; }
    }
}
