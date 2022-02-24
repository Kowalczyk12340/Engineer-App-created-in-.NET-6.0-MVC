using System;
using System.Collections.Generic;
using System.Text;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;

namespace EngineerApplication.ContextStructure.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EngineerDbContext _db;

        public UnitOfWork(EngineerDbContext db)
        {
            _db = db;
            Category = new CategoryService(_db);
            Frequency = new FrequencyService(_db);
            Service = new ServiceRepository(_db);
            OrderHeader = new OrderHeaderService(_db);
            OrderDetails = new OrderDetailsService(_db);
            User = new UserService(_db);
            SP_Call = new SP_Call(_db);
        }

        public ICategoryService Category { get; private set; }
        public IFrequencyService Frequency { get; private set; }
        public IServiceService Service { get; private set; }
        public IOrderHeaderService OrderHeader { get; private set; }
        public IOrderDetailsService OrderDetails { get; private set; }
        public IUserService User { get; private set; }
        public ISP_Call SP_Call { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
