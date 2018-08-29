using System;
using System.Collections.Generic;

namespace BCDS_WebAdmin.Models
{
    public partial class Contracts
    {
        public Contracts()
        {
            Components = new HashSet<Components>();
            ContractManufacturers = new HashSet<ContractManufacturers>();
        }

        public int ContractId { get; set; }
        public string ContractNo { get; set; }
        public string ContractCode { get; set; }
        public string Description { get; set; }
        public int ContractorId { get; set; }
        public string InspectorName { get; set; }

        public virtual Contractors Contractor { get; set; }
        public virtual ICollection<Components> Components { get; set; }
        public virtual ICollection<ContractManufacturers> ContractManufacturers { get; set; }
    }
}
