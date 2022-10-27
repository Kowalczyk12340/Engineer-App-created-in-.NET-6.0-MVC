using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface ICommodityService : IRepository<Commodity>
  {
    IEnumerable<SelectListItem> GetCommodityListForDropDown();
    Task UpdateAsync(Commodity commodity);
  }
}
