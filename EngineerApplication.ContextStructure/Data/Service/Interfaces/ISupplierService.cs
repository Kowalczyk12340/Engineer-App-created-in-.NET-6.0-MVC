using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface ISupplierService : IRepository<Supplier>
  {
    IEnumerable<SelectListItem> GetSupplierListForDropDown();

    void Update(Supplier supplier);
  }
}
