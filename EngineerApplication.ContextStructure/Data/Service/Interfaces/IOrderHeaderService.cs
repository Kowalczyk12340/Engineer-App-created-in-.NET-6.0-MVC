using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IOrderHeaderService : IRepository<OrderHeader>
  {
    Task ChangeOrderStatusAsync(int orderHeaderId, string status);
  }
}
