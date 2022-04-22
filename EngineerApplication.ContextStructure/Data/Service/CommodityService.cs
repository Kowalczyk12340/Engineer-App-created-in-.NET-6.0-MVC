using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
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

    public async Task UpdateAsync(Commodity Commodity)
    {
      var objFromDb = await _db.Commodity.FirstOrDefaultAsync(s => s.Id == Commodity.Id);

      objFromDb.Name = Commodity.Name;
      objFromDb.LongDesc = Commodity.LongDesc;
      objFromDb.Price = Commodity.Price;
      objFromDb.ImageUrl = Commodity.ImageUrl;
      objFromDb.CategoryId = Commodity.CategoryId;

      await _db.SaveChangesAsync();
    }
  }
}
