using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository
{
    public class UserService : Repository<ApplicationUser> , IUserService
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
