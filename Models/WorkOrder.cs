﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public ICollection<WorkOrderEmployee> Employees { get; set; }

        public ICollection<ContractorWorkOrder> Contractors { get; set; }

        public Building Building { get; set; }
        public Equipment Equipment { get; set; }
        public ICollection<WorkOrderTask> Tasks { get; set; }
        public long Cost { get; set; }
        [Display(Name = "To date")]
        public DateTime WorkStart { get; set; }
        [Display(Name = "To date")]
        public DateTime WorkEnd { get; set; }
    }
}
