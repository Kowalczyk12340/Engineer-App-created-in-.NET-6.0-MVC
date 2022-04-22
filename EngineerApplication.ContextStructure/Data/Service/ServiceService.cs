using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

    public async Task UpdateAsync(Entities.Service service)
    {
      var objFromDb = await _db.Service.FirstOrDefaultAsync(s => s.Id == service.Id);

      objFromDb.Name = service.Name;
      objFromDb.LongDesc = service.LongDesc;
      objFromDb.Price = service.Price;
      objFromDb.ImageUrl = service.ImageUrl;
      objFromDb.PaymentId = service.PaymentId;
      objFromDb.CategoryId = service.CategoryId;

      await _db.SaveChangesAsync();
    }
  }
}
