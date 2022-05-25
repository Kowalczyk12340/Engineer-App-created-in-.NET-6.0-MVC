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
  public class SupplierControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private SupplierController? _supplierController;
    private ActionContext? _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _supplierController = new SupplierController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostSupplierMethodForPage()
    {
      var supplierItem = new Supplier()
      {
        Name = "Ambro Express",
        City = "Kalisz",
        PhoneNumber = "509892812"
      };
      var supplier = await Client.PostAsJsonAsync("/Admin/Supplier", supplierItem);
      Assert.IsNotNull(supplier.RequestMessage);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new SupplierController(_unitOfWork.Object);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public async Task TestPutSupplierMethodForPage()
    {
      var supplierItem = new Supplier()
      {
        Name = "Simar",
      };
      var supplier = await Client.PostAsJsonAsync("/Admin/Supplier/1", supplierItem);
      Assert.IsNotNull(supplier.RequestMessage);
    }

    [Test]
    public async Task TestDeleteSupplierMethodForPage()
    {
      var supplier = await Client.DeleteAsync("/Admin/Supplier/1");
      Assert.IsNotNull(supplier.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdSupplierMethodForPage()
    {
      var supplier = await Client.GetAsync("/Admin/Supplier/1");
      var result = supplier.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllSupplierMethodForPage()
    {
      var supplier = await Client.GetAsync("/Admin/Supplier");
      Assert.IsNotNull(supplier.Content);
    }

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var supplierItem = new Supplier()
      {
        Name = "Ambro Express",
        City = "Kalisz",
        PhoneNumber = "509892812"
      };
      var supplier = await Client.PostAsJsonAsync("/Admin/Supplier/export", supplierItem);
      Assert.IsNotNull(supplier.RequestMessage);
    }
  }
}
