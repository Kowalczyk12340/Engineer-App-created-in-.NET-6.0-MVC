using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ServiceTest
{
  public class CategoryServiceTest
  {
    private Mock<EngineerDbContext> _dbContext;
    private ICategoryService _categoryService;

    [SetUp]
    public void Setup()
    {
      _dbContext = new Mock<EngineerDbContext>();
      _categoryService = new CategoryService(_dbContext.Object);
    }

    [Test]
    public async Task TestPostCategoryMethodForPage()
    {
      var categories = _categoryService.GetAll();
      Assert.IsNotNull(categories);
    }

    [Test]
    public async Task TestPutCategoryMethodForPage()
    {
    }

    [Test]
    public async Task TestDeleteCategoryMethodForPage()
    {
    }

    [Test]
    public async Task TestGetByIdCategoryMethodForPage()
    {
    }

    [Test]
    public async Task TestGetAllCategoryMethodForPage()
    {
    }
  }
}