using System.Threading.Tasks;
using EngineerApplication.Areas.Customer.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ControllerTest.Offer
{
  public class CartControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private CartController? _cartController;
    private ActionContext? _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _cartController = new CartController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestGetMethodForShoppingCartPage()
    {
      var response = await Client.GetAsync("/Customer/Cart");
      response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null && responseString.Contains("Accept");
      Assert.That(result);
    }
  }
}
