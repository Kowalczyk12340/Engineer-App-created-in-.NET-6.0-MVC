using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ServiceTest
{
  public class CommodityServiceTest
  {
    private Mock<ICommodityService> _commodityService;
    private DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _commodityService = new Mock<ICommodityService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdCommodity(int id)
    {
      var commodity = new Commodity { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Delivery = new Delivery { Name = "Busem", DeliveryDesc = "Super Bus" }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 }, Supplier = new Supplier() { Name = "Ambro Express", City = "Turek" } };
      var resultService = _commodityService.Setup(p => p.Get(id)).Returns(commodity);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostCommodityMethodForPage(int id)
    {
      var commodity1 = new Commodity { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Delivery = new Delivery { Name = "Busem", DeliveryDesc = "Super Bus" }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 }, Supplier = new Supplier() { Name = "Ambro Express", City = "Turek" } };
      _commodityService.Setup(x => x.Add(commodity1)).Verifiable();
      var addedCommodity = _commodityService.Setup(x => x.Get(id)).Returns(commodity1);
      Assert.That(addedCommodity != null);
    }

    [TestCase(1)]
    public void TestPutCommodityMethodForPage(int id)
    {
      var commodity1 = new Commodity { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Delivery = new Delivery { Name = "Busem", DeliveryDesc = "Super Bus" }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 }, Supplier = new Supplier() { Name = "Ambro Express", City = "Turek" } };
      _commodityService.Setup(x => x.Add(commodity1)).Verifiable();
      commodity1.Name = "Super Obuwie";
      _commodityService.Setup(x => x.Update(commodity1)).Verifiable();
      var editedCommodity = _commodityService.Setup(x => x.Get(id)).Returns(commodity1);
      Assert.That(editedCommodity != null);
    }

    [TestCase(1)]
    public void TestDeleteCommodityMethodForPage(int id)
    {
      var commodity1 = new Commodity { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Delivery = new Delivery { Name = "Busem", DeliveryDesc = "Super Bus" }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 }, Supplier = new Supplier() { Name = "Ambro Express", City = "Turek" } };
      _commodityService.Setup(x => x.Add(commodity1)).Verifiable();
      _commodityService.Setup(x => x.Remove(commodity1)).Verifiable();
      var editedCommodity = _commodityService.Setup(x => x.Get(id)).Returns(commodity1);
      Assert.IsNotNull(editedCommodity);
    }
  }
}