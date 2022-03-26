using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.Entities.ViewModels
{
  public class EmployeeVM
  {
    public Employee? Employee { get; set; }
    public IEnumerable<SelectListItem>? ServiceList { get; set; }
  }
}
