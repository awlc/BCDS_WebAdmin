using System;
using System.Collections.Generic;

namespace BCDS_WebAdmin.Models
{
    public partial class ContractManufacturers
    {
        public ContractManufacturers()
        {
            Components = new HashSet<Components>();
        }

        public int ContractManufacturerId { get; set; }
        public int ContractId { get; set; }
        public int ManufacturerId { get; set; }
        public int ComponentTypeId { get; set; }
        public string Remarks { get; set; }

        public virtual ComponentTypes ComponentType { get; set; }
        public virtual Contracts Contract { get; set; }
        public virtual Manufacturers Manufacturer { get; set; }
        public virtual ICollection<Components> Components { get; set; }
    }
}
