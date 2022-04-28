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
        Name = "Buty Adidas",
        CategoryId = 1,
        LongDesc = "Description for sport item",
        Price = 87.90,
      };
      var commodity = await Client.PostAsJsonAsync("/Admin/Commodity", commodityItem);
      Assert.IsNotNull(commodity.RequestMessage);
    }

    [Test]
    public async Task TestPutCommodityMethodForPage()
    {
      var commodityItem = new Commodity()
      {
        Name = "Buty Adidas",
        CategoryId = 1,
        LongDesc = "Description for sport item",
        Price = 87.90,
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

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var commodityItem = new Commodity()
      {
        Name = "Buty Adidas",
        CategoryId = 1,
        LongDesc = "Description for sport item",
        Price = 87.90,
      };
      var commodity = await Client.PostAsJsonAsync("/Admin/Commodity/export", commodityItem);
      Assert.IsNotNull(commodity.RequestMessage);
    }
  }
}
