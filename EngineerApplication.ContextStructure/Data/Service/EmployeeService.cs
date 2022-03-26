using EngineerApplication.Entities;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class EmployeeService : Repository<Employee>, IEmployeeService
  {
    private readonly EngineerDbContext _db;

    public EmployeeService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public void Update(Employee employee)
    {
      var objFromDb = _db.Employee.FirstOrDefault(s => s.Id == employee.Id);

      objFromDb.Name = employee.Name;
      objFromDb.PhoneNumber = employee.PhoneNumber;
      objFromDb.EmailAddress = employee.EmailAddress;
      objFromDb.EmployeeDesc = employee.EmployeeDesc;

      _db.SaveChanges();
    }
  }
}
