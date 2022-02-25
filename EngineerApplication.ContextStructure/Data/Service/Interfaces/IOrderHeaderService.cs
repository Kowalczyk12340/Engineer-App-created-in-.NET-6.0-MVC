using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IOrderHeaderService : IRepository<OrderHeader>
  {
    void ChangeOrderStatus(int orderHeaderId, string status);
  }
}
