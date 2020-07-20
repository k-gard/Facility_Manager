using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Facility
    {
        public int Id  {get; set;}
        public String FacilityName { get; set; }

        public Company Company { get; set; }

        public ICollection<Building> Buildings { get; set; }

      

    }
}
