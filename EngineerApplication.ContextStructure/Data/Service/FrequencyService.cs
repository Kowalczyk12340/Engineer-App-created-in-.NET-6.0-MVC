using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository
{
    public class FrequencyService : Repository<Frequency> , IFrequencyService
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
