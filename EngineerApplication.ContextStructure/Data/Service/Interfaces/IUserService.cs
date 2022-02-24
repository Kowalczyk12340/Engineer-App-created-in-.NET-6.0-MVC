using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Repository.IRepository
{
    public interface IUserService : IRepository<ApplicationUser>
    {
        void LockUser(string userId);

        void UnLockUser(string userId);
    }
}
