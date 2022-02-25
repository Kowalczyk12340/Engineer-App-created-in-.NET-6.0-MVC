using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.Entities.ViewModels
{
  public class CommodityVM
  {
    public Commodity? Commodity { get; set; }
    public IEnumerable<SelectListItem>? CategoryList { get; set; }
    public IEnumerable<SelectListItem>? FrequencyList { get; set; }
    public IEnumerable<SelectListItem>? SupplierList { get; set; }
  }
}