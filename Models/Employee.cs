using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Employee : IdentityUser
    {
      //  public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public ICollection<WorkOrderEmployee> WorkOrders { get; set; }

        public Department Department { get; set; }
        public String EmployeeType { get; set; }
        public double CostPerHour { get; set; }

    }
}
