#nullable disable
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ServiceTest
{
  public class DeliveryServiceTest
  {
    private readonly DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new();
    private Mock<IDeliveryService> _deliveryService;
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _deliveryService = new Mock<IDeliveryService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdDelivery(int id)
    {
      var delivery = new Delivery { Name = "Dostawa za pobraniem", DeliveryDesc = "Super Sprawa" };
      var resultService = _deliveryService.Setup(p => p.GetAsync(id).Result).Returns(delivery);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostDeliveryMethodForPage(int id)
    {
      var delivery1 = new Delivery { Name = "Dostawa za pobraniem", DeliveryDesc = "Super Sprawa" };
      _deliveryService.Setup(x => x.AddAsync(delivery1)).Verifiable();
      var addedDelivery = _deliveryService.Setup(x => x.GetAsync(id).Result).Returns(delivery1);
      Assert.That(addedDelivery != null);
    }

    [TestCase(1)]
    public void TestPutDeliveryMethodForPage(int id)
    {
      var delivery1 = new Delivery { Name = "Dostawa za pobraniem", DeliveryDesc = "Super Sprawa" };
      _deliveryService.Setup(x => x.AddAsync(delivery1)).Verifiable();
      delivery1.Name = "Super Obuwie";
      _deliveryService.Setup(x => x.UpdateAsync(delivery1)).Verifiable();
      var editedDelivery = _deliveryService.Setup(x => x.GetAsync(id).Result).Returns(delivery1);
      Assert.That(editedDelivery != null);
    }

    [TestCase(1)]
    public void TestDeleteDeliveryMethodForPage(int id)
    {
      var delivery1 = new Delivery { Name = "Dostawa za pobraniem", DeliveryDesc = "Super Sprawa" };
      _deliveryService.Setup(x => x.AddAsync(delivery1)).Verifiable();
      _deliveryService.Setup(x => x.Remove(delivery1)).Verifiable();
      var editedDelivery = _deliveryService.Setup(x => x.GetAsync(id).Result).Returns(delivery1);
      Assert.IsNotNull(editedDelivery);
    }
  }
}