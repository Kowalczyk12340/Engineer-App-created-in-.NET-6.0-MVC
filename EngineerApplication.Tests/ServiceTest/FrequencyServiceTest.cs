using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class FrequencyServiceTest
  {
    private Mock<IFrequencyService> _frequencyService;
    private DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _frequencyService = new Mock<IFrequencyService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdFrequency(int id)
    {
      var frequency = new Frequency { Name = "Dużo", FrequencyCount = 10 };
      var resultService = _frequencyService.Setup(p => p.Get(id)).Returns(frequency);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostFrequencyMethodForPage(int id)
    {
      var frequency1 = new Frequency { Name = "Dużo", FrequencyCount = 10 }; _frequencyService.Setup(x => x.Add(frequency1)).Verifiable();
      var addedFrequency = _frequencyService.Setup(x => x.Get(id)).Returns(frequency1);
      Assert.That(addedFrequency != null);
    }

    [TestCase(1)]
    public void TestPutFrequencyMethodForPage(int id)
    {
      var frequency1 = new Frequency { Name = "Dużo", FrequencyCount = 10 }; _frequencyService.Setup(x => x.Add(frequency1)).Verifiable();
      frequency1.Name = "Super Obuwie";
      _frequencyService.Setup(x => x.Update(frequency1)).Verifiable();
      var editedFrequency = _frequencyService.Setup(x => x.Get(id)).Returns(frequency1);
      Assert.That(editedFrequency != null);
    }

    [TestCase(1)]
    public void TestDeleteFrequencyMethodForPage(int id)
    {
      var frequency1 = new Frequency { Name = "Dużo", FrequencyCount = 10 }; _frequencyService.Setup(x => x.Add(frequency1)).Verifiable();
      _frequencyService.Setup(x => x.Remove(frequency1)).Verifiable();
      var editedFrequency = _frequencyService.Setup(x => x.Get(id)).Returns(frequency1);
      Assert.IsNotNull(editedFrequency);
    }
  }
}