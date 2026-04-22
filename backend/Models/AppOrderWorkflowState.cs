using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class AppOrderWorkflowState
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual CustomerOrder Order { get; set; } = null!;
}
