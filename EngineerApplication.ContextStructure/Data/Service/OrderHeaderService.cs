﻿using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class OrderHeaderService : Repository<OrderHeader>, IOrderHeaderService
  {
    private readonly EngineerDbContext _db;

    public OrderHeaderService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public async Task ChangeOrderStatusAsync(int orderHeaderId, string status)
    {
      var orderFromDb = await _db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
#pragma warning disable CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
      orderFromDb.Status = status;
#pragma warning restore CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
      await _db.SaveChangesAsync();
    }
  }
}
