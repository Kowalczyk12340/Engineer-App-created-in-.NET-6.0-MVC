#nullable disable
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
  public class OfferCommodityController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private OfferCommodityViewModel OfferCommodityVM;

    [BindProperty]
    public CommodityVM CommodityVM { get; set; }

    public OfferCommodityController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> OfferCommodity()
    {
      OfferCommodityVM = new OfferCommodityViewModel()
      {
        CategoryList = await _unitOfWork.Category.GetAllAsync(),
        CommodityList = await _unitOfWork.Commodity.GetAllAsync(includeProperties: "Category"),
      };

      return View("OfferCommodity", OfferCommodityVM);
    }

    [HttpGet("{SearchBox}")]
    public IActionResult SearchCommodities(string SearchBox)
    {
      var commodities = from s in (_unitOfWork.Commodity.GetAllAsync().Result)
                        select s;

      if (!string.IsNullOrEmpty(SearchBox))
      {
        commodities = commodities.Where(s => s.Name!.Contains(SearchBox));
      }

      return View(commodities.ToList());
    }

      [HttpPost]
    public async Task<IActionResult> UpdateAmount(int id, int amount)
    {
      var objFromDb = await _unitOfWork.Commodity.GetFirstOrDefaultAsync(includeProperties: "Category", filter: c => c.Id == id);

      objFromDb.Amount = amount;

      await _unitOfWork.SaveAsync();
      return RedirectToAction(nameof(OfferCommodity));
    }

    public async Task<IActionResult> Details(int id)
    {
      var CommodityFromDb = await _unitOfWork.Commodity.GetFirstOrDefaultAsync(includeProperties: "Category", filter: c => c.Id == id);
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

      return RedirectToAction(nameof(OfferCommodity));
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
