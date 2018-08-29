using System;
using System.Collections.Generic;

namespace BCDS_WebAdmin.Models
{
    public partial class ComponentTypes
    {
        public ComponentTypes()
        {
            Components = new HashSet<Components>();
            ContractManufacturers = new HashSet<ContractManufacturers>();
        }

        public int ComponentTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Components> Components { get; set; }
        public virtual ICollection<ContractManufacturers> ContractManufacturers { get; set; }
    }
}
