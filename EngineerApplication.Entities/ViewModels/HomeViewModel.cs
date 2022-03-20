namespace EngineerApplication.Entities.ViewModels
{
  public class HomeViewModel
  {
    public IEnumerable<Category>? CategoryList { get; set; }
    public IEnumerable<Commodity>? CommodityList { get; set; }
    public IEnumerable<Service>? ServiceList { get; set; }
  }
}
