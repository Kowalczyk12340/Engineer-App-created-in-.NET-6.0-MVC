#nullable disable
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class OrderHeaderService : Repository<OrderHeader>, IOrderHeaderService
  {
    private readonly EngineerDbContext _db;

    public OrderHeaderService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public async Task ChangeOrderStatusAsync(int orderHeaderId, string status)
    {
      var orderFromDb = await _db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
      orderFromDb.Status = status;
      await _db.SaveChangesAsync();
    }
  }
}
