using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class SupplierService : Repository<Supplier>, ISupplierService
  {
    private readonly EngineerDbContext _db;

    public SupplierService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetSupplierListForDropDown()
    {
      return _db.Supplier.Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });
    }

    public void Update(Supplier supplier)
    {
      var objFromDb = _db.Supplier.FirstOrDefault(s => s.Id == supplier.Id);

      objFromDb.Name = supplier.Name;
      objFromDb.City = supplier.City;
      objFromDb.Street = supplier.Street;
      objFromDb.PhoneNumber = supplier.PhoneNumber;
      objFromDb.EmailAddress = supplier.EmailAddress;
      objFromDb.PostalCode = supplier.PostalCode;

      _db.SaveChanges();

    }
  }
}
