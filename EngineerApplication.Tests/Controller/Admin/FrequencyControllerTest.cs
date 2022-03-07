using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.Controller.Admin
{
  public class FrequencyControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork> _unitOfWork;
    private FrequencyController _frequencyController;
    private ActionContext _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _frequencyController = new FrequencyController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostFrequencyMethodForPage()
    {
      var frequencyItem = new Frequency()
      {
        Name = "Szczotki Sportowe"
      };
      var frequency = await Client.PostAsJsonAsync("/Admin/Frequency", frequencyItem);
      Assert.IsNotNull(frequency.RequestMessage.Content);
    }

    [Test]
    public async Task TestPutFrequencyMethodForPage()
    {
      var frequencyItem = new Frequency()
      {
        Name = "Szczotki Sportowe",
      };
      var frequency = await Client.PostAsJsonAsync("/Admin/Frequency/1", frequencyItem);
      Assert.IsNotNull(frequency.RequestMessage);
    }

    [Test]
    public async Task TestDeleteFrequencyMethodForPage()
    {
      var frequency = await Client.DeleteAsync("/Admin/Frequency/1");
      Assert.IsNotNull(frequency.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdFrequencyMethodForPage()
    {
      var frequency = await Client.GetAsync("/Admin/Frequency/1");
      var result = frequency.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllFrequencyMethodForPage()
    {
      var frequency = await Client.GetAsync("/Admin/Frequency");
      Assert.IsNotNull(frequency.Content);
    }
  }
}
