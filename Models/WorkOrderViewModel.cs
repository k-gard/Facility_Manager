using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class WorkOrderViewModel
    {
        public int Id { get; set; }
        [BindProperty]
        public List<String> Employee { get; set; }
        [BindProperty]
        public List<int> Contractor { get; set; }

        public int Building { get; set; }

        public List<int> Tasks { get; set; }
        public long Cost { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy HH:mm:ss}")]
        public DateTime WorkStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy HH:mm:ss}")]
        public DateTime WorkEnd { get; set; }
    }
}
