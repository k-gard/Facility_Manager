using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class ContractorViewModel
    {
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
    }
}
