using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class OfferService : Repository<Offer>, IOfferService
  {
    private readonly EngineerDbContext _db;

    public OfferService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public void Update(Offer offer)
    {
      var objFromDb = _db.Offer.FirstOrDefault(s => s.Id == offer.Id);

      objFromDb.Name = offer.Name;
      objFromDb.Count = offer.Count;
      objFromDb.OfferDesc = offer.OfferDesc;
      objFromDb.CommodityId = offer.CommodityId;

      _db.SaveChanges();
    }
  }
}
