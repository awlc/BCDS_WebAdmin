using System;
using System.Collections.Generic;

namespace BCDS_WebAdmin.Models
{
    public partial class ComponentItems
    {
        public int ComponentItemId { get; set; }
        public int ComponentId { get; set; }
        public string SerialNo { get; set; }
        public string TagId { get; set; }
        public string Wing { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string InspectionGrading { get; set; }
        public string Remark { get; set; }
        public DateTime? TaggingTimestamp { get; set; }
        public DateTime? InspectionTimestamp { get; set; }
        public DateTime? ManufacturingTimestamp { get; set; }
        public DateTime? DeliveryTimestamp { get; set; }
        public DateTime? ReceivingTimestamp { get; set; }
        public DateTime? InstallationTimestamp { get; set; }

        public virtual Components Component { get; set; }
    }
}
