using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.Extensions;
using EngineerApplication.Entities;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class OfferController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private OfferViewModel OfferVM;

    public OfferController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      OfferVM = new OfferViewModel()
      {
        CategoryList = _unitOfWork.Category.GetAll(),
        CommodityList = _unitOfWork.Commodity.GetAll(includeProperties: "Category"),
        ServiceList = _unitOfWork.Service.GetAll(includeProperties: "Category,Payment"),
        EmployeeList = _unitOfWork.Employee.GetAll(includeProperties: "Service"),
      };

      foreach (var commodity in (_unitOfWork.Commodity.GetAll(includeProperties: "Category")))
      {
        var CommodityFromDb = commodity;
        _unitOfWork.Commodity.UpdateCommodityAmount(CommodityFromDb);
        _unitOfWork.Save();
      }

      _unitOfWork.Save();

      return View(OfferVM);
    }

    [HttpPost]
    public IActionResult UpdateAmount(int id)
    {
      var CommodityFromDb = _unitOfWork.Commodity.GetFirstOrDefault(includeProperties: "Category", filter: c => c.Id == id);
      _unitOfWork.Commodity.UpdateCommodityAmount(CommodityFromDb);
      _unitOfWork.Save();
      return View(CommodityFromDb);
    }

    public IActionResult Details(int id)
    {
      var CommodityFromDb = _unitOfWork.Commodity.GetFirstOrDefault(includeProperties: "Category", filter: c => c.Id == id);
      return View(CommodityFromDb);
    }


    public IActionResult AddToCart(int CommodityId)
    {
      List<int> sessionList = new();
      if (string.IsNullOrEmpty(HttpContext.Session.GetString(UsefulConsts.SessionCart)))
      {
        sessionList.Add(CommodityId);
        HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);
      }
      else
      {
        sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
        if (!sessionList.Contains(CommodityId))
        {
          sessionList.Add(CommodityId);
          HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);
        }
      }

      return RedirectToAction(nameof(Index));
    }


    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
