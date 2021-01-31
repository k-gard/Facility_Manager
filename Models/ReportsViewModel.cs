using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class ReportsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Client")]
        public int CompanyId { get; set; }
        [Display(Name = "From date")]
        public DateTime fromDate { get; set; }
        [Display(Name = "To date")]
        public DateTime toDate { get; set; }
    }
}
