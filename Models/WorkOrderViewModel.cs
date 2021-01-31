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
        [Display(Name = "Employee Names")]
        public List<String> EmployeeNames { get; set; }
        [BindProperty]
        public List<int> Contractor { get; set; }
        [Display(Name = "Contractor Names")]
        public List<String> ContractorNames { get; set; }
        public int Building { get; set; }
        [Display(Name = "Building Name")]
        public String BuildingName { get; set; }
        public int Equipment { get; set; }
        [Display(Name = "Equipment")]
        public String EquipmentName { get; set; }
        public List<int> Tasks { get; set; }
        [Display(Name = "Task")]
        public List<String> TasksName { get; set; }
        public long Cost { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy HH:mm:ss}")]
        public DateTime WorkStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy HH:mm:ss}")]
        public DateTime WorkEnd { get; set; }
    }
}
