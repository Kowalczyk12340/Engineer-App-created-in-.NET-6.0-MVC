#nullable disable
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using EngineerApplication.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class OrderHeaderService : Repository<OrderHeader>, IOrderHeaderService
  {
    private readonly EngineerDbContext _db;

    public OrderHeaderService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public async Task ChangeOrderStatusAsync(int orderHeaderId, string status)
    {
      var orderFromDb = await _db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
      orderFromDb.Status = status;
      await _db.SaveChangesAsync();

      var emailReceiver = (await _db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderHeaderId)).Email;

      var email = new Email(new EmailParams
      {
        HostSmtp = "smtp.gmail.com",
        Port = 587,
        EnableSsl = true,
        SenderName = "Administrator Dropshipping Application",
        SenderEmail = "marcinkowalczyk24.7@gmail.com",
        SenderEmailPassword = "vkaqksszjcxkhkym"
      });

      await email.Send(
                $"E'mail z potwierdzeniem zmiany statusu zamówienia na {status}",
                $"Wysłano z aplikacji Application Dropshipping w celu potwierdzenia zmiany statusu zamówienia na {status}, dnia {DateTime.UtcNow}. W sprawie kontaktu z administratorem prosimy o kontakt mailowy.",
                emailReceiver);
    }
  }
}
