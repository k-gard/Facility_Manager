using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<ContractorContract> Contractors { get; set; }
        [Display(Name = "Starting Date")]
        public DateTime StartingDate { get; set; }
        [Display(Name = "Ending Date")]
        public DateTime EndingDate { get; set; }
        [Display(Name = "Award Amount")]
        public int AwardAmount { get; set; }



    }
}
