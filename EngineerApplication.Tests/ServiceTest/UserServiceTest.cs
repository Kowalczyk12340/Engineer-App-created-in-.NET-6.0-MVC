using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ServiceTest
{
  public class UserServiceTest
  {
    private Mock<IUnitOfWork> _unitOfWork;
    private UserController _userController;
    private ActionContext _context;

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
    }

    [Test]
    public async Task TestPutUserMethodForPage()
    {
    }

    [Test]
    public async Task TestDeleteUserMethodForPage()
    {
    }

    [Test]
    public async Task TestGetByIdUserMethodForPage()
    {
    }

    [Test]
    public async Task TestGetAllUserMethodForPage()
    {
    }
  }
}
