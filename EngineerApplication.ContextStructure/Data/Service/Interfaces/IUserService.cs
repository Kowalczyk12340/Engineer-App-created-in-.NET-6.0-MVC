using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IUserService : IRepository<ApplicationUser>
  {
    Task LockUserAsync(string userId);

    Task UnLockUserAsync(string userId);
  }
}
