using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class ContractorContract
    {
        public Contract Contract { get; set; }
        public int ContractId { get; set; }

        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }



    }
}
