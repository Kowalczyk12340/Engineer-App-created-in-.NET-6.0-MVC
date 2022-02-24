namespace EngineerApplication.Entities.ViewModels
{
  public class HomeViewModel
  {
    public IEnumerable<Category>? CategoryList { get; set; }
    public IEnumerable<Commodity>? CommodityList { get; set; }
  }
}
