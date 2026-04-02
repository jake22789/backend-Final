using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Bin
{
    public int Id { get; set; }

    public int? BinType { get; set; }

    public virtual ICollection<BinLocation> BinLocations { get; set; } = new List<BinLocation>();

    public virtual BinType? BinTypeNavigation { get; set; }
}
