using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Bin1
{
    public int Id { get; set; }

    public int? BinType { get; set; }

    public virtual ICollection<BinLocation1> BinLocation1s { get; set; } = new List<BinLocation1>();

    public virtual BinType1? BinTypeNavigation { get; set; }
}
