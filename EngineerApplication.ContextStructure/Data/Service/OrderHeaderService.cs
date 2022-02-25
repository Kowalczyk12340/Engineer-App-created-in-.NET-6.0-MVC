using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class OrderHeaderService : Repository<OrderHeader>, IOrderHeaderService
  {
    private readonly EngineerDbContext _db;

    public OrderHeaderService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public void ChangeOrderStatus(int orderHeaderId, string status)
    {
      var orderFromDb = _db.OrderHeader.FirstOrDefault(o => o.Id == orderHeaderId);
      orderFromDb.Status = status;
      _db.SaveChanges();
    }
  }
}
