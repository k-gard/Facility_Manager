using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class WorkOrder
    {
        public int Id { get; set; } 
        public List<Employee> Employees { get; set; }

        public ICollection<ContractorWorkOrder> Contractors { get; set; }

        public Building Building { get; set; }

        public ICollection<WorkOrderTask> Tasks { get; set; }
        public long Cost { get; set; }

        public DateTime WorkStart { get; set; }

        public DateTime WorkEnd { get; set; }
    }
}
