using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface ICategoryService : IRepository<Category>
  {
    IEnumerable<SelectListItem> GetCategoryForCommodityListForDropDown();

    IEnumerable<SelectListItem> GetCategoryForServiceListForDropDown();

    Task UpdateAsync(Category category);
  }
}
