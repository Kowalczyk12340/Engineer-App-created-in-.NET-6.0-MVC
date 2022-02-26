using Microsoft.AspNetCore.Mvc.Rendering;

namespace EngineerApplication.Entities.ViewModels
{
  public class OfferVM
  {
    public Offer? Offer { get; set; }
    public IEnumerable<SelectListItem>? CommodityList { get; set; }
  }
}
