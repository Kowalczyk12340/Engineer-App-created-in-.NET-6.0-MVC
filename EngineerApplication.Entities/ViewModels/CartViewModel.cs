using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.Entities.ViewModels
{
  public class CartViewModel
  {
    public IEnumerable<Category>? CategoryList { get; set; }
    public IEnumerable<Service>? ServiceList { get; set; }
    public IEnumerable<Employee>? EmployeeList { get; set; }
    public IEnumerable<OrderDetails>? OrderDetails { get; set; }
    public IEnumerable<SelectListItem>? DeliveryList { get; set; }
    public IEnumerable<SelectListItem>? PaymentList { get; set; }
    public IEnumerable<SelectListItem>? SupplierList { get; set; }
    public IList<Commodity>? CommodityList { get; set; }
    public OrderHeader? OrderHeader { get; set; }
    public int Amount { get; set; }
  }
}
