using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IUserService : IRepository<ApplicationUser>
  {
    void LockUser(string userId);

    void UnLockUser(string userId);
  }
}
