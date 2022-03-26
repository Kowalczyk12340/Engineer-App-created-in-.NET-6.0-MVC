using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.Entities.ViewModels
{
  public class ServiceVM
  {
    public Service? Service { get; set; }
    public IEnumerable<SelectListItem>? CategoryList { get; set; }
    public IEnumerable<SelectListItem>? FrequencyList { get; set; }
  }
}
