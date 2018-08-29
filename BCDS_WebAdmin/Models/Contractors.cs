using System;
using System.Collections.Generic;

namespace BCDS_WebAdmin.Models
{
    public partial class Contractors
    {
        public Contractors()
        {
            Contracts = new HashSet<Contracts>();
        }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string ContractorCode { get; set; }

        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}
