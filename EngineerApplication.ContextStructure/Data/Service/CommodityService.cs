#nullable disable
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class CommodityService : Repository<Commodity>, ICommodityService
  {
    private readonly EngineerDbContext _db;

    public CommodityService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetCommodityListForDropDown()
    {
      return _db.Commodity.Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });
    }

    public async Task UpdateAsync(Commodity commodity)
    {
      var objFromDb = await _db.Commodity
        .FirstOrDefaultAsync(s => s.Id == commodity.Id);

      objFromDb.Name = commodity.Name;
      objFromDb.LongDesc = commodity.LongDesc;
      objFromDb.Price = commodity.Price;
      objFromDb.ImageUrl = commodity.ImageUrl;
      objFromDb.CategoryId = commodity.CategoryId;

      await _db.SaveChangesAsync();
    }
  }
}
