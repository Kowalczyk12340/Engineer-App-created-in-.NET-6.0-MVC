#nullable disable
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using System.Diagnostics;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class OfferServiceController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private OfferServiceViewModel OfferServiceVM;

    [BindProperty]
    public CommodityVM CommodityVM { get; set; }

    public OfferServiceController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> OfferService()
    {
      OfferServiceVM = new OfferServiceViewModel()
      {
        CategoryList = await _unitOfWork.Category.GetAllAsync(),
        ServiceList = await _unitOfWork.Service.GetAllAsync(includeProperties: "Category,Payment"),
        EmployeeList = await _unitOfWork.Employee.GetAllAsync(includeProperties: "Service"),
      };

      return View("OfferService", OfferServiceVM);
    }

    [HttpGet("{SearchBox}")]
    public IActionResult SearchServices(string SearchBox)
    {
      var services = from s in (_unitOfWork.Service.GetAllAsync().Result)
                        select s;

      if (!string.IsNullOrEmpty(SearchBox))
      {
        services = services.Where(s => s.Name!.Contains(SearchBox));
      }

      return View(services.ToList());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
