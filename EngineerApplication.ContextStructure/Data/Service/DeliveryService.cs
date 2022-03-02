using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public void Update(Delivery delivery)
    {
      var objFromDb = _db.Delivery.FirstOrDefault(s => s.Id == delivery.Id);

      objFromDb.Name = delivery.Name;
      objFromDb.DeliveryDesc = delivery.DeliveryDesc;

      _db.SaveChanges();
    }
  }
}
