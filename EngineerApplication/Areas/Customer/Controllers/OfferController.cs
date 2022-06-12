#nullable disable
using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class OfferController : Controller
  {
    public IActionResult Index()
    {
      return View("Index");
    }
  }
}
