using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class ContractorWorkOrder
    {
        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public int WorkOrderId { get; set; }
    }
}
