﻿using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class FrequencyService : Repository<Frequency>, IFrequencyService
  {
    private readonly EngineerDbContext _db;

    public FrequencyService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
    {
      return _db.Frequency.Select(i => new SelectListItem()
      {
        Text = i.Name,
        Value = i.Id.ToString()
      });
    }

    public void Update(Frequency frequency)
    {
      var objFromDb = _db.Frequency.FirstOrDefault(s => s.Id == frequency.Id);

      objFromDb.Name = frequency.Name;
      objFromDb.FrequencyCount = frequency.FrequencyCount;

      _db.SaveChanges();
    }

  }
}