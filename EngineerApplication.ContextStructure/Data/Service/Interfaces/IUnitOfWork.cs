using System;
using System.Collections.Generic;
using System.Text;

namespace EngineerApplication.ContextStructure.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryService Category { get; }
        IFrequencyService Frequency { get; }
        IServiceService Service { get; }
        IOrderHeaderService OrderHeader { get; }
        IOrderDetailsService OrderDetails { get; }

        ISP_Call SP_Call { get; }

        IUserService User { get; }
        void Save();
    }
}
