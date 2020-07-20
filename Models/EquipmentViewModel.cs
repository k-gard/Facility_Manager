using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }
        public String Category { get; set; }
        public String Description { get; set; }
        [Display(Name = "Frequency Type")]
        public String FrequencyType { get; set; }
        [Display(Name = "Maintenance Frequency")]
        public int MaintenanceFrequency { get; set; }
        [Display(Name = "Building")]
        public int BuildingId { get; set; }
    }
}
