using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Heberb
{
    public int Id { get; set; }

    public int? AId { get; set; }

    public string? Description { get; set; }

    public virtual Hebera? AIdNavigation { get; set; }
}
