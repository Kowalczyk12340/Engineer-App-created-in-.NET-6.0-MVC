using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class ServiceServiceTest
  {
    private Mock<IServiceService> _serviceService;
    private DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _serviceService = new Mock<IServiceService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdService(int id)
    {
      var service = new Entities.Service { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      var resultService = _serviceService.Setup(p => p.Get(id)).Returns(service);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostServiceMethodForPage(int id)
    {
      var service1 = new Entities.Service { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      _serviceService.Setup(x => x.Add(service1)).Verifiable();
      var addedEntities = _serviceService.Setup(x => x.Get(id)).Returns(service1);
      Assert.That(addedEntities != null);
    }

    [TestCase(1)]
    public void TestPutServiceMethodForPage(int id)
    {
      var service1 = new Entities.Service { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      _serviceService.Setup(x => x.Add(service1)).Verifiable();
      service1.Name = "Super Obuwie";
      _serviceService.Setup(x => x.Update(service1)).Verifiable();
      var editedEntities = _serviceService.Setup(x => x.Get(id)).Returns(service1);
      Assert.That(editedEntities != null);
    }

    [TestCase(1)]
    public void TestDeleteServiceMethodForPage(int id)
    {
      var service1 = new Entities.Service { Name = "Sportowe Obuwie", Frequency = new Frequency { Name = "Big", FrequencyCount = 15 }, Category = new Category { Name = "Super Kategoria", DisplayOrder = 3 } };
      _serviceService.Setup(x => x.Add(service1)).Verifiable();
      _serviceService.Setup(x => x.Remove(service1)).Verifiable();
      var editedService = _serviceService.Setup(x => x.Get(id)).Returns(service1);
      Assert.IsNotNull(editedService);
    }
  }
}