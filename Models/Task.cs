using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }
        public ICollection<WorkOrderTask> WorkOrders { get; set; }


    }
}
