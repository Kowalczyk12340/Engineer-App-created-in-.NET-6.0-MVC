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

    [SetUp]
    public void Setup()
    {
      _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Test]
    public async Task TestPostEmployeeMethodForPage()
    {
      var employeeItem = new Employee()
      {
        Name = "Marek Jurecki",
        Service = new Service()
        {
          Name = "Wycinanie węży do wody na konkretne wymiary i podłączanie ich"
        }
      };
      var employee = await Client.PostAsJsonAsync("/Admin/Employee", employeeItem);
      Assert.IsNotNull(employee.RequestMessage);
    }

    [Test]
    public void ReturnSuitableViewNameForIndex()
    {
      var controller = new EmployeeController(_unitOfWork.Object);
      var result = controller.Index() as ViewResult;
      Assert.AreEqual("Index", result.ViewName);
    }

    [Test]
    public async Task TestPutEmployeeMethodForPage()
    {
      var employeeItem = new Employee()
      {
        Name = "Marek Jurecki",
        Service = new Service()
        {
          Name = "Wycinanie węży do wody na konkretne wymiary i podłączanie ich"
        }
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

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var employeeItem = new Employee()
      {
        Name = "Marek Jurecki",
        Service = new Service()
        {
          Name = "Wycinanie węży do wody na konkretne wymiary i podłączanie ich"
        }
      };
      var employee = await Client.PostAsJsonAsync("/Admin/Employee/export", employeeItem);
      Assert.IsNotNull(employee.RequestMessage);
    }
  }
}
