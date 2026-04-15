using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Shelf1
{
    public int Id { get; set; }

    public int? BayId { get; set; }

    public int? ShelfNumber { get; set; }

    public int? Height { get; set; }

    public virtual Bay1? Bay { get; set; }

    public virtual ICollection<BinLocation> BinLocations { get; set; } = new List<BinLocation>();
}
