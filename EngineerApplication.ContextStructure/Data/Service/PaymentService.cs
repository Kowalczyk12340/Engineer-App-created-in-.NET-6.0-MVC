using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task UpdateAsync(Payment payment)
    {
      var objFromDb = await _db.Payment.FirstOrDefaultAsync(s => s.Id == payment.Id);

      objFromDb.Name = payment.Name;
      objFromDb.Code = payment.Code;

      await _db.SaveChangesAsync();
    }
  }
}
