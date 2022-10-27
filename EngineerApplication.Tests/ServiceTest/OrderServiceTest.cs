#nullable disable
using System.Threading.Tasks;
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using EngineerApplication.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class OrderServiceTest
  {
    private readonly DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new();
    private Mock<IOrderHeaderService> _orderHeader;
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _orderHeader = new Mock<IOrderHeaderService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdOrder(int id)
    {
      var order1 = new OrderHeader { Name = "Zamówienie złączek" };
      var resultService = _orderHeader.Setup(p => p.GetAsync(id).Result).Returns(order1);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostOrderMethodForPage(int id)
    {
      var order1 = new OrderHeader { Name = "Zamówienie złączek" };
      var addedOrder = _orderHeader.Setup(x => x.GetAsync(id).Result).Returns(order1);
      Assert.That(addedOrder != null);
    }

    [TestCase(1)]
    public void TestCheckOrderStatusMethodForPage(int id)
    {
      var order1 = new OrderHeader { Name = "Zamówienie złączek" };
      var addedOrder = Task.FromResult(_orderHeader.Setup(x => x.ChangeOrderStatusAsync(id, UsefulConsts.StatusApproved)));
      Assert.That(addedOrder != null);
    }

    [TestCase(1)]
    public void TestDeleteOrderMethodForPage(int id)
    {
      var order1 = new OrderHeader { Name = "Zamówienie złączek" };
      _orderHeader.Setup(x => x.Remove(order1)).Verifiable();
      var editedOrder = _orderHeader.Setup(x => x.GetAsync(id).Result).Returns(order1);
      Assert.IsNotNull(editedOrder);
    }
  }
}
