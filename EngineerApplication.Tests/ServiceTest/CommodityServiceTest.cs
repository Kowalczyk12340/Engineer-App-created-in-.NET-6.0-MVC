using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class CommodityServiceTest
  {
    private Mock<ICommodityService>? _commodityService;
    private DbContextOptionsBuilder<EngineerDbContext>? _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext>? _options;

    [SetUp]
    public void Setup()
    {
      _commodityService = new Mock<ICommodityService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdCommodity(int id)
    {
      var commodity = new Commodity { Name = "Sportowe Obuwie",  Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      var resultService = _commodityService.Setup(p => p.Get(id)).Returns(commodity);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostCommodityMethodForPage(int id)
    {
      var commodity1 = new Commodity { Name = "Sportowe Obuwie", Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      _commodityService.Setup(x => x.Add(commodity1)).Verifiable();
      var addedCommodity = _commodityService.Setup(x => x.Get(id)).Returns(commodity1);
      Assert.That(addedCommodity != null);
    }

    [TestCase(1)]
    public void TestPutCommodityMethodForPage(int id)
    {
      var commodity1 = new Commodity { Name = "Sportowe Obuwie", Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      _commodityService.Setup(x => x.Add(commodity1)).Verifiable();
      commodity1.Name = "Super Obuwie";
      _commodityService.Setup(x => x.Update(commodity1)).Verifiable();
      var editedCommodity = _commodityService.Setup(x => x.Get(id)).Returns(commodity1);
      Assert.That(editedCommodity != null);
    }

    [TestCase(1)]
    public void TestDeleteCommodityMethodForPage(int id)
    {
      var commodity1 = new Commodity { Name = "Sportowe Obuwie", Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      _commodityService.Setup(x => x.Add(commodity1)).Verifiable();
      _commodityService.Setup(x => x.Remove(commodity1)).Verifiable();
      var editedCommodity = _commodityService.Setup(x => x.Get(id)).Returns(commodity1);
      Assert.IsNotNull(editedCommodity);
    }
  }
}