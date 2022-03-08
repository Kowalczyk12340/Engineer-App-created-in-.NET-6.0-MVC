using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ServiceTest
{
  public class CommodityServiceTest
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
    }

    [Test]
    public async Task TestPutCommodityMethodForPage()
    {
    }

    [Test]
    public async Task TestDeleteCommodityMethodForPage()
    {
    }

    [Test]
    public async Task TestGetByIdCommodityMethodForPage()
    {
    }

    [Test]
    public async Task TestGetAllCommodityMethodForPage()
    {
    }
  }
}
