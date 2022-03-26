using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class ServiceService : Repository<Entities.Service>, IServiceService
  {
    private readonly EngineerDbContext _db;

    public ServiceService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetServiceListForDropDown()
    {
      return _db.Service.Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });
    }

    public void Update(Entities.Service service)
    {
      var objFromDb = _db.Service.FirstOrDefault(s => s.Id == service.Id);

      objFromDb.Name = service.Name;
      objFromDb.LongDesc = service.LongDesc;
      objFromDb.Price = service.Price;
      objFromDb.ImageUrl = service.ImageUrl;
      objFromDb.FrequencyId = service.FrequencyId;
      objFromDb.CategoryId = service.CategoryId;

      _db.SaveChanges();
    }
  }
}
