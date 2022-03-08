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
  public class OfferServiceTest
  {
    private Mock<IUnitOfWork> _unitOfWork;
    private OfferController _offerController;
    private ActionContext _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _offerController = new OfferController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostOfferMethodForPage()
    {
    }

    [Test]
    public async Task TestPutOfferMethodForPage()
    {
    }

    [Test]
    public async Task TestDeleteOfferMethodForPage()
    {
    }

    [Test]
    public async Task TestGetByIdOfferMethodForPage()
    {
    }

    [Test]
    public async Task TestGetAllOfferMethodForPage()
    {
    }
  }
}
