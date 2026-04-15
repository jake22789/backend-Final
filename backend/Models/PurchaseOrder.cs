using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class PurchaseOrder
{
    public int Id { get; set; }

    public int? VendorId { get; set; }

    public DateTime? DateOrdered { get; set; }

    public virtual ICollection<PurchaseOrderItem1> PurchaseOrderItem1s { get; set; } = new List<PurchaseOrderItem1>();

    public virtual ICollection<Shipment1> Shipment1s { get; set; } = new List<Shipment1>();

    public virtual Vendor? Vendor { get; set; }
}
