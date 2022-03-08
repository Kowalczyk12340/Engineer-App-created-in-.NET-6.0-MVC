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
  public class FrequencyServiceTest
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
    }

    [Test]
    public async Task TestPutFrequencyMethodForPage()
    {
    }

    [Test]
    public async Task TestDeleteFrequencyMethodForPage()
    {
    }

    [Test]
    public async Task TestGetByIdFrequencyMethodForPage()
    {
    }

    [Test]
    public async Task TestGetAllFrequencyMethodForPage()
    {
    }
  }
}
