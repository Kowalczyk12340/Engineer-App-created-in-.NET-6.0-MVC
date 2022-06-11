#nullable disable
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class DeliveryService : Repository<Delivery>, IDeliveryService
  {
    private readonly EngineerDbContext _db;

    public DeliveryService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetDeliveryListForDropDown()
    {
      return _db.Delivery.Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });
    }

    public async Task UpdateAsync(Delivery delivery)
    {
      var objFromDb = await _db.Delivery.FirstOrDefaultAsync(s => s.Id == delivery.Id);

      objFromDb.Name = delivery.Name;
      objFromDb.DeliveryDesc = delivery.DeliveryDesc;

      await _db.SaveChangesAsync();
    }
  }
}
