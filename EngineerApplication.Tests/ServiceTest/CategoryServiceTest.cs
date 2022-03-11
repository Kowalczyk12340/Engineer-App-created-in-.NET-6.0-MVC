using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EngineerApplication.Tests.ServiceTest
{
  public class CategoryServiceTest
  {
    private Mock<ICategoryService> _categoryService;
    private DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new DbContextOptionsBuilder<EngineerDbContext>();
    private DbContextOptions<EngineerDbContext> _options;

    [SetUp]
    public void Setup()
    {
      _categoryService = new Mock<ICategoryService>();
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
    }

    [TestCase(1)]
    public void TestGetByIdCategory(int id)
    {
      var category = new Category { Name = "Sportowe Obuwie", DisplayOrder = 8 };
      var resultService = _categoryService.Setup(p => p.Get(id)).Returns(category);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostCategoryMethodForPage(int id)
    {
      var category1 = new Category { Name = "Eleganckie Obuwie", DisplayOrder = 9 };
      _categoryService.Setup(x => x.Add(category1)).Verifiable();
      var addedCategory = _categoryService.Setup(x => x.Get(id)).Returns(category1);
      Assert.That(addedCategory != null);
    }

    [TestCase(1)]
    public void TestPutCategoryMethodForPage(int id)
    {
      var category1 = new Category { Name = "Eleganckie Obuwie", DisplayOrder = 9 };
      _categoryService.Setup(x => x.Add(category1)).Verifiable();
      category1.Name = "Super Obuwie";
      _categoryService.Setup(x => x.Update(category1)).Verifiable();
      var editedCategory = _categoryService.Setup(x => x.Get(id)).Returns(category1);
      Assert.That(editedCategory != null);
    }

    [TestCase(1)]
    public void TestDeleteCategoryMethodForPage(int id)
    {
      var category1 = new Category { Name = "Eleganckie Obuwie", DisplayOrder = 9 };
      _categoryService.Setup(x => x.Add(category1)).Verifiable();
      _categoryService.Setup(x => x.Remove(category1)).Verifiable();
      var editedCategory = _categoryService.Setup(x => x.Get(id)).Returns(category1);
      Assert.IsNotNull(editedCategory);
    }
  }
}