using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ControllerTest.Admin
{
  public class WebImageControllerTest
  {
    private EngineerDbContext _dbContext;
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _options = new DbContextOptions<EngineerDbContext>();
      _dbContext = new EngineerDbContext(_options);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new WebImageController(_dbContext);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }
  }
}
