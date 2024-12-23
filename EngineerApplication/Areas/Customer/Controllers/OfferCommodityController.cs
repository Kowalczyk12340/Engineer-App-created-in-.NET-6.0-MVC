﻿#nullable disable
using System.Diagnostics;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Extensions;
using EngineerApplication.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class OfferCommodityController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private OfferCommodityViewModel OfferCommodityVM;
    private OfferCommodityViewModel OfferCommodityVMWithFilter;

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

      var commodities = OfferCommodityVM.CommodityList.ToList();

      foreach (var commodity in commodities)
      {
        commodity.Amount = 1;
      }

      return View("OfferCommodity", OfferCommodityVM);
    }

    [HttpGet("SearchString")]
    public async Task<IActionResult> OfferCommodityWithFilter(string SearchString)
    {
      OfferCommodityVMWithFilter = new()
      {
        CategoryList = await _unitOfWork.Category.GetAllAsync(),
        CommodityList = await _unitOfWork.Commodity
          .GetAllAsync(includeProperties: "Category"),
      };

      var resultCommodities = OfferCommodityVMWithFilter.CommodityList
        .Where(x => x.Name.ToLower()
        .Contains(SearchString, StringComparison.OrdinalIgnoreCase));

      foreach (var resultCommodity in resultCommodities)
      {
        resultCommodity.Amount = 1;
      }

      var result = new OfferCommodityViewModel()
      {
        CategoryList = await _unitOfWork.Category.GetAllAsync(),
        CommodityList = resultCommodities
      };

      return View("OfferCommodityWithFilter", result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Details(int id, int amount)
    {
      var CommodityFromDb = await _unitOfWork.Commodity.GetFirstOrDefaultAsync(includeProperties: "Category", filter: c => c.Id == id);

      CommodityFromDb.Amount = amount;

      await _unitOfWork.SaveAsync();

      return View(CommodityFromDb);
    }

    public IActionResult AddToCart(int commodityId)
    {
      List<int> sessionList = new();
      if (string.IsNullOrEmpty(HttpContext.Session.GetString(UsefulConsts.SessionCart)))
      {
        sessionList.Add(commodityId);
        HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);
      }
      else
      {
        sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
        if (!sessionList.Contains(commodityId))
        {
          sessionList.Add(commodityId);
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