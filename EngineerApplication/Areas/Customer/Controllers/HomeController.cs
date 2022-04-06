using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
