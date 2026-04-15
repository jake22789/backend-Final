using System;
using System.Collections.Generic;

namespace final_proj.Models;

public partial class Customer1
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<CustomerOrder1> CustomerOrder1s { get; set; } = new List<CustomerOrder1>();
}
