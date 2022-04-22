using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class UserServiceTest
  {
    private readonly DbContextOptionsBuilder<EngineerDbContext>? _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private Mock<IUserService>? _userService;
    private DbContextOptions<EngineerDbContext>? _options;

    [SetUp]
    public void Setup()
    {
      _userService = new Mock<IUserService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdApplicationUser(int id)
    {
      var user = new ApplicationUser { Name = "Marcin", UserName = "Kowalczyk12340", Email = "ambro@ambro.pl", PasswordHash = "Marcingrafik1#" };
      var resultService = _userService.Setup(p => p.GetAsync(id).Result).Returns(user);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostApplicationUserMethodForPage(int id)
    {
      var user1 = new ApplicationUser { Name = "Marcin", UserName = "Kowalczyk12340", Email = "ambro@ambro.pl", PasswordHash = "Marcingrafik1#" };
      var addedApplicationUser = _userService.Setup(x => x.GetAsync(id).Result).Returns(user1);
      Assert.That(addedApplicationUser != null);
    }

    [TestCase(1)]
    public void TestDeleteApplicationUserMethodForPage(int id)
    {
      var user1 = new ApplicationUser { Name = "Marcin", UserName = "Kowalczyk12340", Email = "ambro@ambro.pl", PasswordHash = "Marcingrafik1#" };
      _userService.Setup(x => x.Remove(user1)).Verifiable();
      var editedApplicationUser = _userService.Setup(x => x.GetAsync(id).Result).Returns(user1);
      Assert.IsNotNull(editedApplicationUser);
    }
  }
}