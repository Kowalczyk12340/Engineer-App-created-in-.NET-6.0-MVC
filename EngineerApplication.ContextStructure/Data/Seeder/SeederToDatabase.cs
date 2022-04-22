using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

    public async Task HighlightDatabaseAsync()
    {
      try
      {
        if (_db.Database.GetPendingMigrations().Any())
        {
          await _db.Database.MigrateAsync();
        }
      }
      catch (Exception)
      {
        throw new Exception();
      }

      if (await _db.Roles.AnyAsync(r => r.Name == UsefulConsts.Admin)) return;

      _roleManager.CreateAsync(new IdentityRole(UsefulConsts.Admin)).GetAwaiter().GetResult();
      _roleManager.CreateAsync(new IdentityRole(UsefulConsts.Customer)).GetAwaiter().GetResult();

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

      ApplicationUser? user = await _db.ApplicationUser?.Where(u => u.Email == "marcinkowalczyk24.7@gmail.com").FirstOrDefaultAsync();
      _userManager.AddToRoleAsync(user, UsefulConsts.Admin).GetAwaiter().GetResult();

      ApplicationUser? user1 = await _db.ApplicationUser?.Where(u => u.Email == "marcinkowalczyk24.5@wp.pl").FirstOrDefaultAsync();
      _userManager.AddToRoleAsync(user1, UsefulConsts.Customer).GetAwaiter().GetResult();
    }
  }
}
