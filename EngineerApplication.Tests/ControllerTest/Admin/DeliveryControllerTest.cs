using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ControllerTest.Admin
{
  public class DeliveryControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private DeliveryController? _deliveryController;
    private ActionContext? _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _deliveryController = new DeliveryController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostDeliveryMethodForPage()
    {
      var deliveryItem = new Delivery()
      {
        Name = "Dostawa Taxi",
      };
      var delivery = await Client.PostAsJsonAsync("/Admin/Delivery", deliveryItem);
      Assert.IsNotNull(delivery.RequestMessage);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new DeliveryController(_unitOfWork.Object);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public async Task TestPutDeliveryMethodForPage()
    {
      var deliveryItem = new Delivery()
      {
        Name = "Dostawa Taxi",
      };
      var delivery = await Client.PostAsJsonAsync("/Admin/Delivery/1", deliveryItem);
      Assert.IsNotNull(delivery.RequestMessage);
    }

    [Test]
    public async Task TestDeleteDeliveryMethodForPage()
    {
      var delivery = await Client.DeleteAsync("/Admin/Delivery/1");
      Assert.IsNotNull(delivery.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdDeliveryMethodForPage()
    {
      var delivery = await Client.GetAsync("/Admin/Delivery/1");
      var result = delivery.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllDeliveryMethodForPage()
    {
      var delivery = await Client.GetAsync("/Admin/Delivery");
      Assert.IsNotNull(delivery.Content);
    }

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var deliveryItem = new Delivery()
      {
        Name = "Dostawa Taxi",
      };
      var delivery = await Client.PostAsJsonAsync("/Admin/Commodity/export", deliveryItem);
      Assert.IsNotNull(delivery.RequestMessage);
    }
  }
}
