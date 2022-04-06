using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using EngineerApplication.Tests.ControllerTest;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ControllerTest.Admin
{
  public class UserControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private UserController? _userController;
    private ActionContext? _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _userController = new UserController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostUserMethodForPage()
    {
      var userItem = new ApplicationUser()
      {
        Name = "Marcin Kowalczyk",
        Email = "marcinkowalczyk11@wp.pl",
        PasswordHash = "Marcingrafik1#",
        City = "Kalisz",
        PhoneNumber = "506093843"
      };
      var user = await Client.PostAsJsonAsync("/Admin/User", userItem);
      Assert.IsNotNull(user.RequestMessage);
    }

    [Test]
    public async Task TestPutUserMethodForPage()
    {
      var userItem = new ApplicationUser()
      {
        Name = "Marcin Kowalczyk",
        Email = "marcinkowalczyk11@wp.pl",
        PasswordHash = "Marcingrafik1#",
        City = "Kalisz",
        PhoneNumber = "506093843"
      };
      var user = await Client.PostAsJsonAsync("/Admin/User/1", userItem);
      Assert.IsNotNull(user.RequestMessage);
    }

    [Test]
    public async Task TestDeleteUserMethodForPage()
    {
      var user = await Client.DeleteAsync("/Admin/User/1");
      Assert.IsNotNull(user.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdUserMethodForPage()
    {
      var user = await Client.GetAsync("/Admin/User/1");
      var result = user.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllUserMethodForPage()
    {
      var user = await Client.GetAsync("/Admin/User");
      Assert.IsNotNull(user.Content);
    }
  }
}
