﻿#nullable disable
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class CategoryService : Repository<Category>, ICategoryService
  {
    private readonly EngineerDbContext _db;
    public CategoryService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetCategoryForCommodityListForDropDown()
    {
      var result = _db.Category.Where(x => x.IsForCommodity).Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });

      return result;
    }

    public IEnumerable<SelectListItem> GetCategoryForServiceListForDropDown()
    {
      var result = _db.Category.Where(x => !x.IsForCommodity).Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });

      return result;
    }

    public async Task UpdateAsync(Category category)
    {
      var objFromDb = await _db.Category.FirstOrDefaultAsync(s => s.Id == category.Id);

      objFromDb.Name = category.Name;
      objFromDb.DisplayOrder = category.DisplayOrder;

      await _db.SaveChangesAsync();
    }
  }
}
