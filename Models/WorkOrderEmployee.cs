using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class WorkOrderEmployee
    {
        public Employee Employee { get; set; }
        public String EmployeeId { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public int WorkOrderId { get; set; }
    }
}
