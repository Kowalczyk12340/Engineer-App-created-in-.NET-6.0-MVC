namespace EngineerApplication.Entities.ViewModels
{
  public class CartViewModel
  {
    public IList<Commodity>? CommodityList { get; set; }
    public IList<Service>? ServiceList { get; set; }
    public OrderHeader? OrderHeader { get; set; }
  }
}
