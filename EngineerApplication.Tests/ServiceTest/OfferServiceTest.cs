using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class OfferServiceTest
  {
    private Mock<IOfferService> _offerService;
    private DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _offerService = new Mock<IOfferService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdOffer(int id)
    {
      var offer = new Offer { Name = "Dużo", OfferDesc = "Spora oferta", Count = 2};
      var resultService = _offerService.Setup(p => p.Get(id)).Returns(offer);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostOfferMethodForPage(int id)
    {
      var offer1 = new Offer { Name = "Dużo", OfferDesc = "Spora oferta", Count = 2 };
      var addedOffer = _offerService.Setup(x => x.Get(id)).Returns(offer1);
      Assert.That(addedOffer != null);
    }

    [TestCase(1)]
    public void TestPutOfferMethodForPage(int id)
    {
      var offer1 = new Offer { Name = "Dużo", OfferDesc = "Spora oferta", Count = 2 };
      offer1.Name = "Super Obuwie";
      _offerService.Setup(x => x.Update(offer1)).Verifiable();
      var editedOffer = _offerService.Setup(x => x.Get(id)).Returns(offer1);
      Assert.That(editedOffer != null);
    }

    [TestCase(1)]
    public void TestDeleteOfferMethodForPage(int id)
    {
      var offer1 = new Offer { Name = "Dużo", OfferDesc = "Spora oferta", Count = 2 };
      _offerService.Setup(x => x.Remove(offer1)).Verifiable();
      var editedOffer = _offerService.Setup(x => x.Get(id)).Returns(offer1);
      Assert.IsNotNull(editedOffer);
    }
  }
}