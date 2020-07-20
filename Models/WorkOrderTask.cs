using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class WorkOrderTask
    {
        public Task Task { get; set; }
        public int TaskId { get; set; }

        public WorkOrder WorkOrder { get; set; }
        public int WorkOrderId { get; set; }

    }
}
