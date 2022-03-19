using EngineerApplication.ContextStructure.Data.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IServiceService : IRepository<Entities.Service>
  {
    IEnumerable<SelectListItem> GetServiceListForDropDown();
    void Update(Entities.Service Service);
  }
}

