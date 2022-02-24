using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository.IRepository
{
  public interface ICommodityService : IRepository<Commodity>
  {
    void Update(Commodity Commodity);
  }
}
