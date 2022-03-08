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
  public class SupplierServiceTest
  {
    private Mock<IUnitOfWork> _unitOfWork;
    private SupplierController _supplierController;
    private ActionContext _context;

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
    }

    [Test]
    public async Task TestPutSupplierMethodForPage()
    {
    }

    [Test]
    public async Task TestDeleteSupplierMethodForPage()
    {
    }

    [Test]
    public async Task TestGetByIdSupplierMethodForPage()
    {
    }

    [Test]
    public async Task TestGetAllSupplierMethodForPage()
    {
    }
  }
}
