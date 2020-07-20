using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{ 
    public class Equipment
    {
        public int Id { get; set; }
        public String Category { get; set; }
        public String Description { get; set; }
        public String FrequencyType { get; set; }
        public int MaintenanceFrequency { get; set; }
        public Building Building { get; set; }
    }

   
}
