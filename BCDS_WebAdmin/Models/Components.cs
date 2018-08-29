using System;
using System.Collections.Generic;

namespace BCDS_WebAdmin.Models
{
    public partial class Components
    {
        public Components()
        {
            // commented out since EF Core doesn't have option to disable proxy creation
            //ComponentItems = new HashSet<ComponentItems>();
        }

        public int ComponentId { get; set; }
        public int ContractManufacturerId { get; set; }
        public string TypeNo { get; set; }
        public int? ContractQuantity { get; set; }
        public int? TaggedQuantity { get; set; }
        public int? ProductionCompletedQuantity { get; set; }
        public int? DeliveredQuantity { get; set; }
        public int? ReceivedQuantity { get; set; }
        public int? BuiltQuantity { get; set; }
        public string Discriminator { get; set; }
        public int? ComponentTypeComponentTypeId { get; set; }
        public int? ContractContractId { get; set; }

        public virtual ComponentTypes ComponentTypeComponentType { get; set; }
        public virtual Contracts ContractContract { get; set; }
        public virtual ContractManufacturers ContractManufacturer { get; set; }
        // commented out since EF Core doesn't have option to disable proxy creation
        //public ICollection<ComponentItems> ComponentItems { get; set; }
    }
}
