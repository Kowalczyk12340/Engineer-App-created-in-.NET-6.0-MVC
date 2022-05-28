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
  public class CategoryControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private CategoryController? _categoryController;
    private ActionContext? _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _categoryController = new CategoryController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostCategoryMethodForPage()
    {
      var categoryItem = new Category()
      {
        Name = "Szczotki Druciane",
        DisplayOrder = 8
      };
      var category = await Client.PostAsJsonAsync("/Admin/Category", categoryItem);
      Assert.IsNotNull(category.RequestMessage);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new CategoryController(_unitOfWork.Object);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public async Task TestPutCategoryMethodForPage()
    {
      var categoryItem = new Category()
      {
        Name = "Szczotki Sportowe",
        DisplayOrder = 9
      };
      var category = await Client.PostAsJsonAsync("/Admin/Category/1", categoryItem);
      Assert.IsNotNull(category.RequestMessage);
    }

    [Test]
    public async Task TestDeleteCategoryMethodForPage()
    {
      var category = await Client.DeleteAsync("/Admin/Category/1");
      Assert.IsNotNull(category.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdCategoryMethodForPage()
    {
      var category = await Client.GetAsync("/Admin/Category/1");
      var result = category.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllCategoryMethodForPage()
    {
      var category = await Client.GetAsync("/Admin/Category");
      Assert.IsNotNull(category.Content);
    }

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var categoryItem = new Category()
      {
        Name = "Szczotki Sportowe",
        DisplayOrder = 8
      };
      var category = await Client.PostAsJsonAsync("/Admin/Category/export", categoryItem);
      Assert.IsNotNull(category.RequestMessage);
    }
  }
}