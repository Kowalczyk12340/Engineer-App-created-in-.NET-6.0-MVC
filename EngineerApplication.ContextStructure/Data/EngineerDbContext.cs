using EngineerApplication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data
{
  public class EngineerDbContext : IdentityDbContext
  {
    public EngineerDbContext(DbContextOptions<EngineerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category>? Category { get; set; }
    public DbSet<Payment>? Payment { get; set; }
    public DbSet<Supplier>? Supplier { get; set; }
    public DbSet<Entities.Service>? Service { get; set; }
    public DbSet<Delivery>? Delivery { get; set; }
    public DbSet<Commodity>? Commodity { get; set; }
    public DbSet<Employee>? Employee { get; set; }
    public DbSet<WebImages>? WebImages { get; set; }
    public DbSet<ApplicationUser>? ApplicationUser { get; set; }
    public DbSet<OrderHeader>? OrderHeader { get; set; }
    public DbSet<OrderDetails>? OrderDetails { get; set; }
  }
}