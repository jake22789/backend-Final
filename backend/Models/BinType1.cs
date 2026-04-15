using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class BinType1
{
    public int Id { get; set; }

    public int? Height { get; set; }

    public int? Length { get; set; }

    public int? Width { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Bin1> Bin1s { get; set; } = new List<Bin1>();
}
