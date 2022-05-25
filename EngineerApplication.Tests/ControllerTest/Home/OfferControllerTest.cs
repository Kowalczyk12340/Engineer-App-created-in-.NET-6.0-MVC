using EngineerApplication.Areas.Customer.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ControllerTest.Offer
{
  public class OfferControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Test]
    public async Task TestGetMethodForOfferPage()
    {
      var response = await Client.GetAsync("/Customer/Offer");
      response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null && responseString.Contains("");
      Assert.That(result);
    }

    [Test]
    public async Task TestLoginMethodForOfferPage()
    {
      var response = await Client.GetAsync("/Account/Login");
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null || responseString.ToLower().Contains("email");
      Assert.That(result);
    }

    [Test]
    public async Task TestCartMethodForOfferPage()
    {
      var response = await Client.GetAsync("/Cart");
      var responseString = await response.Content.ReadAsStringAsync();
      var result = (responseString != null);
      Assert.That(result);
    }
  }
}
