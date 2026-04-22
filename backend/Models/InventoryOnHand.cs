using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class InventoryOnHand
{
    public int? Id { get; set; }

    public string? ItemName { get; set; }

    public long? QtyInWarehouse { get; set; }

    public long? QtySoldNotYetShipped { get; set; }

    public long? QtyAvailableForSale { get; set; }
}
