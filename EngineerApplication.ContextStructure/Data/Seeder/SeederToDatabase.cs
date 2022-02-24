using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using EngineerApplication.Entities;
using EngineerApplication.Helpers;

namespace EngineerApplication.ContextStructure.Data.Initializer
{
  public class SeederToDatabase : ISeederToDatabase
  {
    private readonly EngineerDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeederToDatabase(EngineerDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      _db = db;
      _roleManager = roleManager;
      _userManager = userManager;
    }

    public void HighlightDatabase()
    {
      try
      {
        if (_db.Database.GetPendingMigrations().Any())
        {
          _db.Database.Migrate();
        }
      }
      catch (Exception)
      {
        throw new Exception();
      }

      if (_db.Roles.Any(r => r.Name == UsefulConsts.Admin)) return;

      _roleManager.CreateAsync(new IdentityRole(UsefulConsts.Admin)).GetAwaiter().GetResult();
      _roleManager.CreateAsync(new IdentityRole(UsefulConsts.Employee)).GetAwaiter().GetResult();

      _userManager.CreateAsync(new ApplicationUser
      {
        UserName = "marcinkowalczyk24.7@gmail.com",
        Email = "marcinkowalczyk24.7@gmail.com",
        EmailConfirmed = true,
        Name = "Marcin Kowalczyk"

      }, "Marcingrafik1#").GetAwaiter().GetResult();

      _userManager.CreateAsync(new ApplicationUser
      {
        UserName = "marcinkowalczyk24.5@wp.pl",
        Email = "marcinkowalczyk24.5@wp.pl",
        EmailConfirmed = true,
        Name = "Marcin Kowalczyk"

      }, "Marcingrafik1#").GetAwaiter().GetResult();

      ApplicationUser? user = _db.ApplicationUser?.Where(u => u.Email == "marcinkowalczyk24.7@gmail.com").FirstOrDefault();
      _userManager.AddToRoleAsync(user, UsefulConsts.Admin).GetAwaiter().GetResult();

      ApplicationUser? user1 = _db.ApplicationUser?.Where(u => u.Email == "marcinkowalczyk24.5@wp.pl").FirstOrDefault();
      _userManager.AddToRoleAsync(user1, UsefulConsts.Employee).GetAwaiter().GetResult();

    }
  }
}
