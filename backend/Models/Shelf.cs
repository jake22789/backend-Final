using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Shelf
{
    public int Id { get; set; }

    public int? BayId { get; set; }

    public int? ShelfNumber { get; set; }

    public int? Height { get; set; }

    public virtual Bay? Bay { get; set; }

    public virtual ICollection<BinLocation1> BinLocation1s { get; set; } = new List<BinLocation1>();
}
