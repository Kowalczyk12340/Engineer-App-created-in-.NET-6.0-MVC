using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class OrderDetailsService : Repository<OrderDetails>, IOrderDetailsService
  {
    private readonly EngineerDbContext _db;

    public OrderDetailsService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }
  }
}
