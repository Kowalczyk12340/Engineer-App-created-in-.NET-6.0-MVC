using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class PaymentService : Repository<Payment>, IPaymentService
  {
    private readonly EngineerDbContext _db;

    public PaymentService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetPaymentListForDropDown()
    {
      return _db.Payment.Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });
    }

    public void Update(Payment payment)
    {
      var objFromDb = _db.Payment.FirstOrDefault(s => s.Id == payment.Id);

      objFromDb.Name = payment.Name;
      objFromDb.Code = payment.Code;

      _db.SaveChanges();
    }

  }
}
