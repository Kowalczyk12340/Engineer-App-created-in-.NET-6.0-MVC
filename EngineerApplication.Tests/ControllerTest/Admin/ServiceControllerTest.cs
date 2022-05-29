using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ControllerTest.Admin
{
  public class ServiceControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private readonly IWebHostEnvironment? _hostEnvironment;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Test]
    public async Task TestPostServiceMethodForPage()
    {
      var serviceItem = new Service()
      {
        Name = "Skręcanie łap do robotów",
        CategoryId = 1,
        PaymentId = 1,
        LongDesc = "Skręcanie łap do robotów poprzez mechaniczne dostosowanie obiektu",
        Price = 87.90,
      };
      var service = await Client.PostAsJsonAsync("/Admin/Service", serviceItem);
      Assert.IsNotNull(service.RequestMessage);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new ServiceController(_unitOfWork.Object, _hostEnvironment);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public async Task TestPutServiceMethodForPage()
    {
      var serviceItem = new Service()
      {
        Name = "Skręcanie łap do robotów",
        CategoryId = 1,
        PaymentId = 1,
        LongDesc = "Skręcanie łap do robotów poprzez mechaniczne dostosowanie obiektu",
        Price = 87.90,
      };
      var service = await Client.PostAsJsonAsync("/Admin/Service/1", serviceItem);
      Assert.IsNotNull(service.RequestMessage);
    }

    [Test]
    public async Task TestDeleteServiceMethodForPage()
    {
      var service = await Client.DeleteAsync("/Admin/Service/1");
      Assert.IsNotNull(service.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdServiceMethodForPage()
    {
      var service = await Client.GetAsync("/Admin/Service/1");
      var result = service.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllServiceMethodForPage()
    {
      var service = await Client.GetAsync("/Admin/Service");
      Assert.IsNotNull(service.Content);
    }

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var serviceItem = new Service()
      {
        Name = "Skręcanie łap do robotów",
        CategoryId = 1,
        PaymentId = 1,
        LongDesc = "Skręcanie łap do robotów poprzez mechaniczne dostosowanie obiektu",
        Price = 87.90,
      };
      var service = await Client.PostAsJsonAsync("/Admin/Service/export", serviceItem);
      Assert.IsNotNull(service.RequestMessage);
    }
  }
}
