using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Company
    {
        public int Id {get ; set;}
        public String Name { get; set; }
        public long Afm { get; set; }
        [Display(Name = "Address")]
        public String AddressStreet { get; set; }
        [Display(Name = "Street Number")]
        public String AddressNumber { get; set; }
        [Display(Name = "Post Code")]
        public String AddressPostCode { get; set; }
        [Display(Name = "Phone Number")]
        public long PhoneNumber { get; set; }
        public String Email { get; set; }
        public Contractor Contractor { get; set; }
        
        public ICollection<Department> Departments  { get; set; }

        public ICollection<Facility> Facilities { get; set; }



public Company()
        {
        }
    }
}
