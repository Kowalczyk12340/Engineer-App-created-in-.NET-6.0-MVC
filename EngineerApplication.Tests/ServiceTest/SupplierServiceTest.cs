using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class SupplierServiceTest
  {
    private Mock<ISupplierService> _supplierService;
    private DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _supplierService = new Mock<ISupplierService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdSupplier(int id)
    {
      var supplier = new Supplier { Name = "Ambro", City = "Kalisz", EmailAddress = "ambro@ambro.pl" };
      var resultService = _supplierService.Setup(p => p.Get(id)).Returns(supplier);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostSupplierMethodForPage(int id)
    {
      var supplier1 = new Supplier { Name = "Ambro", City = "Kalisz", EmailAddress = "ambro@ambro.pl" };
      var addedSupplier = _supplierService.Setup(x => x.Get(id)).Returns(supplier1);
      Assert.That(addedSupplier != null);
    }

    [TestCase(1)]
    public void TestPutSupplierMethodForPage(int id)
    {
      var supplier1 = new Supplier { Name = "Ambro", City = "Kalisz", EmailAddress = "ambro@ambro.pl" };
      supplier1.Name = "Super Obuwie";
      _supplierService.Setup(x => x.Update(supplier1)).Verifiable();
      var editedSupplier = _supplierService.Setup(x => x.Get(id)).Returns(supplier1);
      Assert.That(editedSupplier != null);
    }

    [TestCase(1)]
    public void TestDeleteSupplierMethodForPage(int id)
    {
      var supplier1 = new Supplier { Name = "Ambro", City = "Kalisz", EmailAddress = "ambro@ambro.pl" };
      _supplierService.Setup(x => x.Remove(supplier1)).Verifiable();
      var editedSupplier = _supplierService.Setup(x => x.Get(id)).Returns(supplier1);
      Assert.IsNotNull(editedSupplier);
    }
  }
}