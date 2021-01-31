using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class ContractViewModel
    {
        
        public string Name { get; set; }
        [Display(Name = "Contractor A")]
        public int  Contractor1 { get; set; }
        [Display(Name = "Contractor B")]
        public int  Contractor2 { get; set; }
        [Display(Name = "Starting Date")]
        public DateTime StartingDate { get; set; }
        [Display(Name = "Ending Date")]
        public DateTime EndingDate { get; set; }
        [Display(Name = "Award Amount")]
        public int AwardAmount { get; set; }

    }
}
