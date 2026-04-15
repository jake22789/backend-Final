using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Aisle
{
    public int Id { get; set; }

    public virtual ICollection<Bay1> Bay1s { get; set; } = new List<Bay1>();
}
