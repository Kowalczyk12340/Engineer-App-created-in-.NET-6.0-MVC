using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.Entities;

namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IEmployeeService : IRepository<Employee>
  {
    void Update(Employee employee);
  }
}
