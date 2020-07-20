using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.Models
{
    public class ContractViewModel
    {
        public string Name { get; set; }
        public int  Contractor1 { get; set; }
        public int  Contractor2 { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public int AwardAmount { get; set; }

    }
}
