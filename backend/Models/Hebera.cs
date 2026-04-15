using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Hebera
{
    public int Id { get; set; }

    public virtual ICollection<Heberb> Heberbs { get; set; } = new List<Heberb>();
}
