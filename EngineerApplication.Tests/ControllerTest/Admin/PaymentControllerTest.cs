using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ControllerTest.Admin
{
  public class PaymentControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Test]
    public async Task TestPostPaymentMethodForPage()
    {
      var paymentItem = new Payment()
      {
        Name = "Płatność Kartą",
        Code = "hdbhenj484"
      };
      var payment = await Client.PostAsJsonAsync("/Admin/Payment", paymentItem);
      Assert.IsNotNull(payment.RequestMessage);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new PaymentController(_unitOfWork.Object);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public async Task TestPutPaymentMethodForPage()
    {
      var paymentItem = new Payment()
      {
        Name = "Płatność Kartą",
        Code = "hdbhenj484"
      };
      var payment = await Client.PostAsJsonAsync("/Admin/Payment/1", paymentItem);
      Assert.IsNotNull(payment.RequestMessage);
    }

    [Test]
    public async Task TestDeletePaymentMethodForPage()
    {
      var payment = await Client.DeleteAsync("/Admin/Payment/1");
      Assert.IsNotNull(payment.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdPaymentMethodForPage()
    {
      var payment = await Client.GetAsync("/Admin/Payment/1");
      var result = payment.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllPaymentMethodForPage()
    {
      var payment = await Client.GetAsync("/Admin/Payment");
      Assert.IsNotNull(payment.Content);
    }

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var paymentItem = new Payment()
      {
        Name = "Płatność Kartą",
        Code = "hdbhenj484"
      };
      var payment = await Client.PostAsJsonAsync("/Admin/Payment/export", paymentItem);
      Assert.IsNotNull(payment.RequestMessage);
    }
  }
}
