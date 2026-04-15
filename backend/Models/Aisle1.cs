using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Aisle1
{
    public int Id { get; set; }

    public virtual ICollection<Bay> Bays { get; set; } = new List<Bay>();
}
