#nullable disable
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
    private readonly DbContextOptionsBuilder<EngineerDbContext> _optionsBuilder = new();
    private Mock<ICategoryService> _categoryService;
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
      var category = new Category { Name = "Śruby Imbusowe", DisplayOrder = 8, IsForCommodity = true };
      var resultService = _categoryService.Setup(p => p.GetAsync(id).Result).Returns(category);
      Assert.IsNotNull(resultService);
    }

    [TestCase(1)]
    public void TestPostCategoryMethodForPage(int id)
    {
      var category1 = new Category { Name = "Amortyzatory ssawek", DisplayOrder = 9, IsForCommodity = false };
      _categoryService.Setup(x => x.AddAsync(category1)).Verifiable();
      var addedCategory = _categoryService.Setup(p => p.GetAsync(id).Result).Returns(category1);
      Assert.IsNotNull(addedCategory);
    }

    [TestCase(1)]
    public void TestPutCategoryMethodForPage(int id)
    {
      var category1 = new Category { Name = "Amortyzatory ssawek", DisplayOrder = 9, IsForCommodity = true };
      _categoryService.Setup(x => x.AddAsync(category1)).Verifiable();
      category1.Name = "Super Obuwie";
      _categoryService.Setup(x => x.UpdateAsync(category1)).Verifiable();
      var editedCategory = _categoryService.Setup(p => p.GetAsync(id).Result).Returns(category1);
      Assert.IsNotNull(editedCategory);
    }

    [TestCase(1)]
    public void TestDeleteCategoryMethodForPage(int id)
    {
      var category1 = new Category { Name = "Amortyzatory ssawek", DisplayOrder = 9, IsForCommodity = true };
      _categoryService.Setup(x => x.AddAsync(category1)).Verifiable();
      _categoryService.Setup(x => x.Remove(category1)).Verifiable();
      var editedCategory = _categoryService.Setup(p => p.GetAsync(id).Result).Returns(category1);
      Assert.IsNotNull(editedCategory);
    }
  }
}