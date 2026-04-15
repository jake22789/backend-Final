using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class PurchaseOrder1
{
    public int Id { get; set; }

    public DateTime? DateOrdered { get; set; }

    public int? VendorId { get; set; }

    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual Vendor1? Vendor { get; set; }
}
