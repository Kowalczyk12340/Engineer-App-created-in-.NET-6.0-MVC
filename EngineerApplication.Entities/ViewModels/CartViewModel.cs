namespace EngineerApplication.Entities.ViewModels
{
  public class CartViewModel
  {
    public IList<Commodity>? CommodityList { get; set; }
    public OrderHeader? OrderHeader { get; set; }
  }
}
