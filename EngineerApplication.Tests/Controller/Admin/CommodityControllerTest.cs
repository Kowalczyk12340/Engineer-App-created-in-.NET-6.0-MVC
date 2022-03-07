using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.Controller.Admin
{
  public class CommodityControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork> _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;
    private CommodityController _commodityController;
    private ActionContext _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _commodityController = new CommodityController(_unitOfWork.Object, _hostEnvironment);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostCommodityMethodForPage()
    {
      var commodityItem = new Commodity()
      {
        Name = "Szczotki Sportowe",
        DisplayOrder = 8
      };
      var commodity = await Client.PostAsJsonAsync("/Admin/Commodity", commodityItem);
      Assert.IsNotNull(commodity.RequestMessage.Content);
    }

    [Test]
    public async Task TestPutCommodityMethodForPage()
    {
      var commodityItem = new Commodity()
      {
        Name = "Szczotki Sportowe",
        DisplayOrder = 9
      };
      var commodity = await Client.PostAsJsonAsync("/Admin/Commodity/1", commodityItem);
      Assert.IsNotNull(commodity.RequestMessage);
    }

    [Test]
    public async Task TestDeleteCommodityMethodForPage()
    {
      var commodity = await Client.DeleteAsync("/Admin/Commodity/1");
      Assert.IsNotNull(commodity.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdCommodityMethodForPage()
    {
      var commodity = await Client.GetAsync("/Admin/Commodity/1");
      var result = commodity.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllCommodityMethodForPage()
    {
      var commodity = await Client.GetAsync("/Admin/Commodity");
      Assert.IsNotNull(commodity.Content);
    }
  }
}
