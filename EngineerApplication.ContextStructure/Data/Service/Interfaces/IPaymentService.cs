using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IPaymentService : IRepository<Payment>
  {
    IEnumerable<SelectListItem> GetPaymentListForDropDown();

    Task UpdateAsync(Payment payment);
  }
}
