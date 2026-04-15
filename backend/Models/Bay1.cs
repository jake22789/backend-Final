using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Bay1
{
    public int Id { get; set; }

    public int? AisleId { get; set; }

    public virtual Aisle? Aisle { get; set; }

    public virtual ICollection<Shelf1> Shelf1s { get; set; } = new List<Shelf1>();
}
