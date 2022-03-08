using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ServiceTest
{
  public class DeliveryServiceTest
  {
    private Mock<IUnitOfWork> _unitOfWork;
    private DeliveryController _deliveryController;
    private ActionContext _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _deliveryController = new DeliveryController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostDeliveryMethodForPage()
    {
    }

    [Test]
    public async Task TestPutDeliveryMethodForPage()
    {
    }

    [Test]
    public async Task TestDeleteDeliveryMethodForPage()
    {
    }

    [Test]
    public async Task TestGetByIdDeliveryMethodForPage()
    {
    }

    [Test]
    public async Task TestGetAllDeliveryMethodForPage()
    {
    }
  }
}
