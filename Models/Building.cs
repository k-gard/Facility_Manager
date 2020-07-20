using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Building
    {
        public int Id { get; set; }
        public String Name { get; set; }
        [Display(Name = "Building Type")]
        public String BuildingType { get; set; }
        public Facility Facility { get; set; }
        
        public  ICollection<Equipment> Equipment { get; set; }
    }
}
