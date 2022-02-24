using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository
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
