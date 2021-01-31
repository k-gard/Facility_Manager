using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Report
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public int TotalWorkOrders { get; set; }
        public float TotalWorkHours { get; set; }
        public float TotalWorkCost { get; set; }
        public int TotalCost { get; set; }

        public DateTime fromDate { get; set; }

        public DateTime toDate { get; set; }
    }
}
