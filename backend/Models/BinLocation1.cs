using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class BinLocation1
{
    public int Id { get; set; }

    public int? ShelfId { get; set; }

    public int? BinId { get; set; }

    public int? ItemId { get; set; }

    public int? Quantity { get; set; }

    public virtual Bin1? Bin { get; set; }

    public virtual Item1? Item { get; set; }

    public virtual Shelf? Shelf { get; set; }
}
