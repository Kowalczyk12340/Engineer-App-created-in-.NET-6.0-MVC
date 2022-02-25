using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class CategoryService : Repository<Category>, ICategoryService
  {
    private readonly EngineerDbContext _db;

    public CategoryService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetCategoryListForDropDown()
    {
      return _db.Category.Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });
    }

    public void Update(Category category)
    {
      var objFromDb = _db.Category.FirstOrDefault(s => s.Id == category.Id);

      objFromDb.Name = category.Name;
      objFromDb.DisplayOrder = category.DisplayOrder;

      _db.SaveChanges();

    }
  }
}
