﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IFrequencyService : IRepository<Frequency>
  {
    IEnumerable<SelectListItem> GetFrequencyListForDropDown();

    void Update(Frequency frequency);
  }
}
