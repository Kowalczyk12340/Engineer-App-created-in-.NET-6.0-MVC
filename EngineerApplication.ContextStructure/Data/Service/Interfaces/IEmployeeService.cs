using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IEmployeeService : IRepository<Employee>
  {
    Task UpdateAsync(Employee employee);
  }
}
