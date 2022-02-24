using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository
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
