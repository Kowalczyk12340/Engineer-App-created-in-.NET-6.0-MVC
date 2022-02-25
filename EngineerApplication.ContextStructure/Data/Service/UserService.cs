using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class UserService : Repository<ApplicationUser>, IUserService
  {
    private readonly EngineerDbContext _db;

    public UserService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public void LockUser(string userId)
    {
      var userFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
      userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
      _db.SaveChanges();
    }

    public void UnLockUser(string userId)
    {
      var userFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
      userFromDb.LockoutEnd = DateTime.Now;
      _db.SaveChanges();
    }
  }
}
