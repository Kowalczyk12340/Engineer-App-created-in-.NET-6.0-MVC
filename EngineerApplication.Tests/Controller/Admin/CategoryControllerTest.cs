using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.Controller.Admin
{
  public class CategoryControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork> _unitOfWork;
    private CategoryController _categoryController;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _categoryController = new CategoryController(_unitOfWork.Object);
    }
    [Test]
    public async Task TestGetMethodForHomePage()
    {
      var response = await Client.GetAsync("/");
      response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null && responseString.Contains("Bluzy Sportowe");
      Assert.That(result);
    }

    [Test]
    public async Task TestPostCategoryMethodForPage()
    {
      var categoryItem = new Category()
      {
        Name = "Szczotki Sportowe",
        DisplayOrder = 8
      };
      var category = await Client.PostAsJsonAsync("/Admin/Controller", categoryItem);
      Assert.IsNotNull(category.RequestMessage.Content);
    }

    [Test]
    public async Task TestPutCategoryMethodForPage()
    {
      var categoryItem = new Category()
      {
        Name = "Szczotki Sportowe",
        DisplayOrder = 9
      };
      var category = await Client.PostAsJsonAsync("/Admin/Controller/1", categoryItem);
      Assert.IsNotNull(category.RequestMessage);
    }

    [Test]
    public async Task TestDeleteCategoryMethodForPage()
    {
      var category = await Client.DeleteAsync("/Admin/Controller/1");
      Assert.IsNotNull(category.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdCategoryMethodForPage()
    {
      var category = await Client.GetAsync("/Admin/Controller/1");
      var result = category.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllCategoryMethodForPage()
    {
      var category = await Client.GetAsync("/Admin/Controller");
      var result = category.RequestMessage;
      Assert.IsNotNull(result);
    }
  }
}