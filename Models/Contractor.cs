using System.Collections.Generic;

namespace FacilityManager.Models
{
    public class Contractor
    {
        public int ContractorId { get; set; }
        public Company Company { get; set; }
        public ICollection<ContractorContract> Contracts { get; set; }

        public ICollection<ContractorWorkOrder> WorkOrders { get; set; }
    }
}