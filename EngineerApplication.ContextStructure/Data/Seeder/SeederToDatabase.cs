#nullable disable
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
        throw new Exception("The migration can not be done");
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

      var user = await _db.ApplicationUser?.Where(u => u.Email == "marcinkowalczyk24.7@gmail.com").FirstOrDefaultAsync();
      _userManager.AddToRoleAsync(user, UsefulConsts.Admin).GetAwaiter().GetResult();

      var user1 = await _db.ApplicationUser?.Where(u => u.Email == "marcinkowalczyk24.5@wp.pl").FirstOrDefaultAsync();
      _userManager.AddToRoleAsync(user1, UsefulConsts.Customer).GetAwaiter().GetResult();

      await _db.Delivery.AddAsync(new Delivery()
      {
        Name = "Przesyłka kurierska pobraniowa",
        DeliveryDesc = "Przesyłka, w której klient odbiera" +
        " przesyłkę w określonym terminie, płacąc za nią przy odbiorze"
      });

      await _db.Commodity.AddAsync(new Commodity
      {
        Name = "Ssawka okrągła FI6",
        Category = new Category() { Name = "Śruby filcowe", DisplayOrder = 0, IsForCommodity = true },
        Amount = 7,
        ImageUrl = "url",
        LongDesc = "Niesamowita ssawka do chwytania części chropowatych poprzez swoją gąbkę",
        Price = 89,
      });

      await _db.Supplier.AddAsync(new Supplier
      {
        Name = "Ambro Express",
        City = "Przykona",
        PostalCode = "62-700 Turek",
        EmailAddress = "ambro-express@ambro.com",
        PhoneNumber = "512334092",
        Street = "Kaliska 11A"
      });

      await _db.WebImages.AddRangeAsync(new WebImages
      {
        Name = "Foto Ssawki FI6",
        Picture = null
      });

      await _db.Employee.AddRangeAsync(
        new List<Employee>
        {
          new Employee()
          {
            Name = "Jarosław Krzak",
            EmailAddress = "jaroslaw.krzak@wp.pl",
            EmployeeDesc = "Jarosław to fantastyczny pracownik, który zajmuje się skrawaniem materiałów metalowych na skrawarce",
            PhoneNumber = "500222185",
            Service = new Entities.Service()
            {
              Name = "Skrawanie metali",
              Category = new Category()
              {
                Name = "Skrawanie",
                DisplayOrder = 1,
                IsForCommodity = false
              },
              ImageUrl = "url",
              Price = 345,
              Payment = new Payment
              {
                Name = "Płatność przelewem tradycyjnym",
                Code = "JDHDFJNDCKF3535"
              },
              LongDesc = "Usługa skrawania metali, do wszelkich różnych, niesamowitych rzeczy",
            }
          },
          new Employee
          {
            Name = "Wojciech Majchrzak",
            EmailAddress = "wojciech.majchrzak@wp.pl",
            EmployeeDesc = "Wojciech to fantastyczny pracownik, który zajmuje się przetwarzaniem materiałów gumowych i tworzyw sztucznych pod ssawki",
            PhoneNumber = "500222185",
            Service = new Entities.Service()
            {
              Name = "Produkcja ssawek na zamówienie",
              Category = new Category()
              {
                Name = "Produkcja ssawek",
                DisplayOrder = 1,
                IsForCommodity = false
              },
              ImageUrl = "url",
              Price = 345,
              Payment = new Payment
              {
                Name = "Płatność przelewem tradycyjnym",
                Code = "JDHDFJNDCKF3535"
              },
              LongDesc = "Usługa skrawania metali, do wszelkich różnych, niesamowitych rzeczy",
            }
          }
        }
      );

      await _db.SaveChangesAsync();
    }
  }
}
