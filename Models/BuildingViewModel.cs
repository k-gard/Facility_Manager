using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class BuildingViewModel
    {

        public int Id { get; set; }
        public String Name { get; set; }
        public String BuildingType { get; set; }
        [Display(Name = "Owner : Facility")]
        public int FacilityId { get; set; }

        public int Equipment;

    }
}
