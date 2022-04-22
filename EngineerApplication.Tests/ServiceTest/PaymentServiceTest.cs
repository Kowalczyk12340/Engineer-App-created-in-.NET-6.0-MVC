using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class PaymentServiceTest
  {
    private readonly DbContextOptionsBuilder<EngineerDbContext>? _optionsBuilder = new();
    private Mock<IPaymentService>? _paymentService;
    private DbContextOptions<EngineerDbContext>? _options;

    [SetUp]
    public void Setup()
    {
      _paymentService = new Mock<IPaymentService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdPayment(int id)
    {
      var payment = new Payment { Name = "Dużo", Code = "njdvcjuern" };
      var resultService = _paymentService.Setup(p => p.GetAsync(id).Result).Returns(payment);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostPaymentMethodForPage(int id)
    {
      var payment1 = new Payment { Name = "Dużo", Code = "njdvcjuern" }; _paymentService.Setup(x => x.AddAsync(payment1)).Verifiable();
      var addedPayment = _paymentService.Setup(x => x.GetAsync(id).Result).Returns(payment1);
      Assert.That(addedPayment != null);
    }

    [TestCase(1)]
    public void TestPutPaymentMethodForPage(int id)
    {
      var payment1 = new Payment { Name = "Dużo", Code = "njdvcjuern" }; _paymentService.Setup(x => x.AddAsync(payment1)).Verifiable();
      payment1.Name = "Super Obuwie";
      _paymentService.Setup(x => x.UpdateAsync(payment1)).Verifiable();
      var editedPayment = _paymentService.Setup(x => x.GetAsync(id).Result).Returns(payment1);
      Assert.That(editedPayment != null);
    }

    [TestCase(1)]
    public void TestDeletePaymentMethodForPage(int id)
    {
      var payment1 = new Payment { Name = "Dużo", Code = "njdvcjuern" }; _paymentService.Setup(x => x.AddAsync(payment1)).Verifiable();
      _paymentService.Setup(x => x.Remove(payment1)).Verifiable();
      var editedPayment = _paymentService.Setup(x => x.GetAsync(id).Result).Returns(payment1);
      Assert.IsNotNull(editedPayment);
    }
  }
}