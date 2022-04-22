using EngineerApplication.ContextStructure.Data;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ServiceTest
{
  public class EmployeeServiceTest
  {
    private readonly DbContextOptionsBuilder<EngineerDbContext>? _optionsBuilder = new();
    private Mock<IEmployeeService>? _employeeService;
    private DbContextOptions<EngineerDbContext>? _options;

    [SetUp]
    public void Setup()
    {
      _employeeService = new Mock<IEmployeeService>();
#pragma warning disable CS8604 // Możliwy argument odwołania o wartości null.
      _options = _optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VPKE3ES\\SQLEXPRESS;Initial Catalog=EngineerDatabase;Integrated Security=True;").Options;
#pragma warning restore CS8604 // Możliwy argument odwołania o wartości null.
    }

    [TestCase(1)]
    public void TestGetByIdEmployee(int id)
    {
      var employee = new Employee { Name = "Karol Wojtczak", EmployeeDesc = "Spora oferta", PhoneNumber = "809786634", EmailAddress = "karol.wojtczak@wp.pl" };
      var resultService = _employeeService.Setup(p => p.GetAsync(id).Result).Returns(employee);
      Assert.That(resultService != null);
    }

    [TestCase(1)]
    public void TestPostEmployeeMethodForPage(int id)
    {
      var employee1 = new Employee { Name = "Wojciech Pająk", EmployeeDesc = "Spora oferta", PhoneNumber = "782630634", EmailAddress = "wojciech.pajak@wp.pl" };
      var addedEmployee = _employeeService.Setup(x => x.GetAsync(id).Result).Returns(employee1);
      Assert.That(addedEmployee != null);
    }

    [TestCase(1)]
    public void TestPutEmployeeMethodForPage(int id)
    {
      var employee1 = new Employee { Name = "Karol Wojtczak", EmployeeDesc = "Spora oferta", PhoneNumber = "809786634", EmailAddress = "karol.wojtczak@wp.pl" };
      employee1.Name = "Super Obuwie";
      _employeeService.Setup(x => x.UpdateAsync(employee1)).Verifiable();
      var editedEmployee = _employeeService.Setup(x => x.GetAsync(id).Result).Returns(employee1);
      Assert.That(editedEmployee != null);
    }

    [TestCase(1)]
    public void TestDeleteEmployeeMethodForPage(int id)
    {
      var employee1 = new Employee { Name = "Wojciech Pająk", EmployeeDesc = "Spora oferta", PhoneNumber = "782630634", EmailAddress = "wojciech.pajak@wp.pl" };
      _employeeService.Setup(x => x.Remove(employee1)).Verifiable();
      var editedEmployee = _employeeService.Setup(x => x.GetAsync(id).Result).Returns(employee1);
      Assert.IsNotNull(editedEmployee);
    }
  }
}