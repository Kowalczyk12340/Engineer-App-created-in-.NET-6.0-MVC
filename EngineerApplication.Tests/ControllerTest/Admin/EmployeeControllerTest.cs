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
  public class EmployeeControllerTest : BaseControllerTest
  {
    private Mock<IUnitOfWork>? _unitOfWork;
    private EmployeeController? _employeeController;
    private ActionContext? _context;

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
      _employeeController = new EmployeeController(_unitOfWork.Object);
      _context = new ActionContext();
    }

    [Test]
    public async Task TestPostEmployeeMethodForPage()
    {
      var employeeItem = new Employee()
      {
        Name = "Fantastic employee for Tesco",
      };
      var employee = await Client.PostAsJsonAsync("/Admin/Employee", employeeItem);
      Assert.IsNotNull(employee.RequestMessage);
    }

    [Test]
    public async Task TestPutEmployeeMethodForPage()
    {
      var employeeItem = new Employee()
      {
        Name = "Fantastic employee for Ambro",
      };
      var employee = await Client.PostAsJsonAsync("/Admin/Employee/1", employeeItem);
      Assert.IsNotNull(employee.RequestMessage);
    }

    [Test]
    public async Task TestDeleteEmployeeMethodForPage()
    {
      var employee = await Client.DeleteAsync("/Admin/Employee/1");
      Assert.IsNotNull(employee.RequestMessage);
    }

    [Test]
    public async Task TestGetByIdEmployeeMethodForPage()
    {
      var employee = await Client.GetAsync("/Admin/Employee/1");
      var result = employee.RequestMessage;
      Assert.IsNotNull(result);
    }

    [Test]
    public async Task TestGetAllEmployeeMethodForPage()
    {
      var employee = await Client.GetAsync("/Admin/Employee");
      Assert.IsNotNull(employee.Content);
    }
  }
}
