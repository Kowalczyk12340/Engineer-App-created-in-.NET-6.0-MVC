using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IPaymentService : IRepository<Payment>
  {
    IEnumerable<SelectListItem> GetPaymentListForDropDown();

    Task UpdateAsync(Payment payment);
  }
}
