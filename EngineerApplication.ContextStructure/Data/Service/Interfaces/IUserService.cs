using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IUserService : IRepository<ApplicationUser>
  {
    Task LockUserAsync(string userId);

    Task UnLockUserAsync(string userId);
  }
}
