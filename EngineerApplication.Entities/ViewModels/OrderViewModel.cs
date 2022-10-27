using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.Entities.ViewModels
{
  public class OrderViewModel
  {
    public OrderHeader? OrderHeader { get; set; }
    public Commodity? Commodity { get; set; }
    public Supplier? Supplier { get; set; }
    public Delivery? Delivery { get; set; }
    public Payment? Payment { get; set; }
    public IEnumerable<OrderDetails>? OrderDetails { get; set; }
    public IEnumerable<SelectListItem>? DeliveryList { get; set; }
    public IEnumerable<SelectListItem>? PaymentList { get; set; }
    public IEnumerable<SelectListItem>? SupplierList { get; set; }
    public int Amount { get; set; }
    public string TimeToOrder { get; set; }
    public string TimeToRealisation { get; set; }
  }
}
