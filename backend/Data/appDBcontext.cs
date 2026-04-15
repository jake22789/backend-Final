using Microsoft.EntityFrameworkCore;
using final_proj.Models;

namespace final_proj.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Vendor> Vendors {get; set;}
}