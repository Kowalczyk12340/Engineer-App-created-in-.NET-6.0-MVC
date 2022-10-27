using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface ICategoryService : IRepository<Category>
  {
    IEnumerable<SelectListItem> GetCategoryForCommodityListForDropDown();

    IEnumerable<SelectListItem> GetCategoryForServiceListForDropDown();

    Task UpdateAsync(Category category);
  }
}
