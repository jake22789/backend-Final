using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class BinLocation
{
    public int Id { get; set; }

    public int? ShelfId { get; set; }

    public int? BinId { get; set; }

    public int? ItemId { get; set; }

    public int? Quantity { get; set; }

    public virtual Bin? Bin { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Shelf1? Shelf { get; set; }
}
