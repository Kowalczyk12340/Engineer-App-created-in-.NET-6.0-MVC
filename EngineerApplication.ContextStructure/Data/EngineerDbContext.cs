using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data
{
  public class EngineerDbContext : IdentityDbContext
  {
    public EngineerDbContext(DbContextOptions<EngineerDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category>? Category { get; set; }
    public DbSet<Frequency>? Frequency { get; set; }
    public DbSet<Supplier>? Supplier { get; set; }
    public DbSet<Delivery>? Delivery { get; set; }
    public DbSet<Commodity>? Commodity { get; set; }
    public DbSet<Offer>? Offer { get; set; }
    public DbSet<WebImages>? WebImages { get; set; }
    public DbSet<ApplicationUser>? ApplicationUser { get; set; }
    public DbSet<OrderHeader>? OrderHeader { get; set; }
    public DbSet<OrderDetails>? OrderDetails { get; set; }
  }
}