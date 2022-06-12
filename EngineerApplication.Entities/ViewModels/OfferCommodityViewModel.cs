namespace EngineerApplication.Entities.ViewModels
{
  public class OfferCommodityViewModel
  {
    public IEnumerable<Category>? CategoryList { get; set; }
    public IEnumerable<Commodity>? CommodityList { get; set; }
    public IQueryable<Commodity>? Commodities { get; set; }
    public string? SearchBox { get; set; }
  }
}
