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
    private OfferServiceViewModel OfferServiceVMWithFilter;

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

    [HttpGet("SearchStringService")]
    public async Task<IActionResult> OfferServiceWithFilter(string SearchStringService)
    {
      OfferServiceVMWithFilter = new OfferServiceViewModel()
      {
        CategoryList = await _unitOfWork.Category.GetAllAsync(),
        ServiceList = await _unitOfWork.Service.GetAllAsync(includeProperties: "Category,Payment"),
        EmployeeList = await _unitOfWork.Employee.GetAllAsync(includeProperties: "Service"),
      };

      var resultServices = new List<Service>();

      if (SearchStringService == null)
      {
        resultServices = OfferServiceVMWithFilter.ServiceList.ToList();
      }
      else
      {
        resultServices = OfferServiceVMWithFilter.ServiceList.Where(x => x.Name.ToLower().Contains(SearchStringService)).ToList();
      }

      var result = new OfferServiceViewModel()
      {
        CategoryList = await _unitOfWork.Category.GetAllAsync(),
        ServiceList = resultServices,
        EmployeeList = await _unitOfWork.Employee.GetAllAsync(includeProperties: "Service"),
      };

      return View("OfferServiceWithFilter", result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
