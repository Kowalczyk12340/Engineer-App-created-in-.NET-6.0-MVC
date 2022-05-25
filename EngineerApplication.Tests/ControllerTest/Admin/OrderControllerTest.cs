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
  public class OrderControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private OrderController? _orderController;
    private ActionContext? _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _orderController = new OrderController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostOrderMethodForPage()
    {
      var orderItem = new OrderHeader()
      {
        Name = "Fantastic order for Tesco",
      };
      var order = await Client.PostAsJsonAsync("/Admin/Order", orderItem);
      Assert.IsNotNull(order.RequestMessage);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new OrderController(_unitOfWork.Object);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public async Task TestPutOrderMethodForPage()
    {
      var orderItem = new OrderHeader()
      {
        Name = "Fantastic order for Ambro",
      };
      var order = await Client.PostAsJsonAsync("/Admin/Order/1", orderItem);
      Assert.IsNotNull(order.RequestMessage);
    }

    [Test]
    public async Task TestDeleteOrderMethodForPage()
    {
      var order = await Client.DeleteAsync("/Admin/Order/1");
      Assert.IsNotNull(order.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdOrderMethodForPage()
    {
      var order = await Client.GetAsync("/Admin/Order/1");
      var result = order.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllOrderMethodForPage()
    {
      var order = await Client.GetAsync("/Admin/Order");
      Assert.IsNotNull(order.Content);
    }

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var orderItem = new OrderHeader()
      {
        Name = "Fantastic order for Tesco",
      };
      var order = await Client.PostAsJsonAsync("/Admin/Order/export", orderItem);
      Assert.IsNotNull(order.RequestMessage);
    }
  }
}
