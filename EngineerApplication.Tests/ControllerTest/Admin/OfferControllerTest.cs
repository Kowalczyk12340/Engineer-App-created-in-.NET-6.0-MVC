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
  public class OfferControllerTest : BaseControllerTest
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
      var offerItem = new Offer()
      {
        Name = "Fantastic offer for Tesco",
      };
      var offer = await Client.PostAsJsonAsync("/Admin/Offer", offerItem);
      Assert.IsNotNull(offer.RequestMessage);
    }

    [Test]
    public async Task TestPutOfferMethodForPage()
    {
      var offerItem = new Offer()
      {
        Name = "Fantastic offer for Ambro",
      };
      var offer = await Client.PostAsJsonAsync("/Admin/Offer/1", offerItem);
      Assert.IsNotNull(offer.RequestMessage);
    }

    [Test]
    public async Task TestDeleteOfferMethodForPage()
    {
      var offer = await Client.DeleteAsync("/Admin/Offer/1");
      Assert.IsNotNull(offer.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdOfferMethodForPage()
    {
      var offer = await Client.GetAsync("/Admin/Offer/1");
      var result = offer.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllOfferMethodForPage()
    {
      var offer = await Client.GetAsync("/Admin/Offer");
      Assert.IsNotNull(offer.Content);
    }
  }
}
