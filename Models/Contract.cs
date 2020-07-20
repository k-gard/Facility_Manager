using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ContractorContract> Contractors { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int AwardAmount { get; set; }



    }
}
