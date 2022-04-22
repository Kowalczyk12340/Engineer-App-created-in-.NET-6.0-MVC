using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IDeliveryService : IRepository<Delivery>
  {
    IEnumerable<SelectListItem> GetDeliveryListForDropDown();

    Task UpdateAsync(Delivery delivery);
  }
}
