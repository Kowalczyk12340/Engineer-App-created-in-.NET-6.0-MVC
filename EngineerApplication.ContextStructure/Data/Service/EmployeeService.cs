#nullable disable
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class EmployeeService : Repository<Employee>, IEmployeeService
  {
    private readonly EngineerDbContext _db;

    public EmployeeService(EngineerDbContext db) : base(db)
    {
      _db = db;
    }

    public async Task UpdateAsync(Employee employee)
    {
      var objFromDb = await _db.Employee.FirstOrDefaultAsync(s => s.Id == employee.Id);

      objFromDb.Name = employee.Name;
      objFromDb.PhoneNumber = employee.PhoneNumber;
      objFromDb.EmailAddress = employee.EmailAddress;
      objFromDb.EmployeeDesc = employee.EmployeeDesc;
      objFromDb.ServiceId = employee.ServiceId;

      await _db.SaveChangesAsync();
    }
  }
}
