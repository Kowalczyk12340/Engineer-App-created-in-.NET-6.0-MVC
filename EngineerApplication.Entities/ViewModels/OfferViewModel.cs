using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Entities.ViewModels
{
  public class OfferViewModel
  {
    public IEnumerable<Category>? CategoryList { get; set; }
    public IEnumerable<Commodity>? CommodityList { get; set; }
    public IEnumerable<Service>? ServiceList { get; set; }
    public IEnumerable<Employee>? EmployeeList { get; set; }
  }
}
