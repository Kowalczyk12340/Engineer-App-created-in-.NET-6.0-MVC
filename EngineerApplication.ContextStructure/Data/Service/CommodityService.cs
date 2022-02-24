using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository
{
  public class CommodityService : Repository<Commodity>, ICommodityService
  {
    private readonly EngineerDbContext _db;

    public CommodityService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }


    public void Update(Commodity Commodity)
    {
      var objFromDb = _db.Commodity.FirstOrDefault(s => s.Id == Commodity.Id);

      objFromDb.Name = Commodity.Name;
      objFromDb.LongDesc = Commodity.LongDesc;
      objFromDb.Price = Commodity.Price;
      objFromDb.ImageUrl = Commodity.ImageUrl;
      objFromDb.FrequencyId = Commodity.FrequencyId;
      objFromDb.CategoryId = Commodity.CategoryId;

      _db.SaveChanges();

    }
  }
}
