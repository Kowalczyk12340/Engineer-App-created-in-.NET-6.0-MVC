using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository
{
    public class OrderHeaderService : Repository<OrderHeader> , IOrderHeaderService
    {
        private readonly EngineerDbContext _db;

        public OrderHeaderService(EngineerDbContext db) : base(db)
        {
            _db = db;
        }

       public void ChangeOrderStatus(int orderHeaderId, string status)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(o => o.Id == orderHeaderId);
            orderFromDb.Status = status;
            _db.SaveChanges();
        }

    }
}
