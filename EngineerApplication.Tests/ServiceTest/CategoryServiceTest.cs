using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EngineerApplication.Tests.ServiceTest
{
  public class CategoryServiceTest
  {
    private Mock<EngineerDbContext> _dbContext;
    private Mock<ICategoryService> _categoryService;
    private DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _categoryService = new Mock<ICategoryService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
      _dbContext = new Mock<EngineerDbContext>(_options);
    }

    [Test]
    public void TestPostCategoryMethodForPage()
    {
      var category = new Category()
      {
        Name = "Narty zjazdowe",
        DisplayOrder = 9
      };
      var categories = _categoryService.Object.GetAll().ToList().Count;
      _categoryService.Object.Add(category);
      var categoriesAfter = _categoryService.Object.GetAll().ToList().Count;
      Assert.True(categories == categoriesAfter + 1);
    }

    [TestCase(100)]
    [TestCase(101)]
    public void TestPutCategoryMethodForPage(int id)
    {
      var category = _categoryService.Object.Get(id);
      category.Name = "Obuwie Sportowe";
      _categoryService.Object.Update(category);
      Assert.True(category.Name.Equals("Obuwie Sportowe"));
    }

    [TestCase(100)]
    [TestCase(101)]
    public void TestDeleteCategoryMethodForPage(int id)
    {
      using (var context = new EngineerDbContext(_options))
      {
        var categories = _categoryService.Object.GetAll().ToList().Count;
        _categoryService.Object.Remove(id);
        var categoriesAfter = _categoryService.Object.GetAll().ToList().Count;
        Assert.IsTrue(categories == categoriesAfter - 1);
      }
    }

    [TestCase(100)]
    [TestCase(101)]
    public void TestGetByIdCategoryMethodForPage(int id)
    {
      var categoryInMemoryDatabase = new List<Category>()
      {
        new Category { Id = 100, Name = "Kaski Sportowe", DisplayOrder = 4 },
        new Category { Id = 101, Name = "Buty Piłkarskie", DisplayOrder = 25 },
        new Category { Id = 102, Name = "Getry Piłkarskie", DisplayOrder = 30 }
      };
      _categoryService.Setup(x => x.Add(categoryInMemoryDatabase.Find(x => x.Id == 100))).Verifiable();
      _categoryService.Setup(x => x.Add(categoryInMemoryDatabase.Find(x => x.Id == 101))).Verifiable();
      _categoryService.Setup(x => x.Add(categoryInMemoryDatabase.Find(x => x.Id == 102))).Verifiable();
      var category = _categoryService.Object.Get(id);
      Assert.IsNotNull(category);
    }

  [Test]
  public void TestGetAllCategoryMethodForPage()
  {
    var categories = _categoryService.Object.GetAll();
    Assert.IsNotNull(categories);
  }
}
}