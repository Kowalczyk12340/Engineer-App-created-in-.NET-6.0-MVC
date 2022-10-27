#nullable disable
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class UserService : Repository<ApplicationUser>, IUserService
  {
    private readonly EngineerDbContext _db;

    public UserService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public async Task LockUserAsync(string userId)
    {
      var userFromDb = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userId);
      userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
      await _db.SaveChangesAsync();
    }

    public async Task UnLockUserAsync(string userId)
    {
      var userFromDb = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userId);
      userFromDb.LockoutEnd = DateTime.Now;
      await _db.SaveChangesAsync();
    }
  }
}
