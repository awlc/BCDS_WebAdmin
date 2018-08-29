using System;
using System.Collections.Generic;

namespace BCDS_WebAdmin.Models
{
    public partial class Manufacturers
    {
        public Manufacturers()
        {
            ContractManufacturers = new HashSet<ContractManufacturers>();
        }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerAddress { get; set; }

        public virtual ICollection<ContractManufacturers> ContractManufacturers { get; set; }
    }
}
