using Microsoft.AspNetCore.Mvc;
using EngineerApplication.Extensions;
using EngineerApplication.Entities;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class CartController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public CartViewModel CartVM { get; set; }

    public CartController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      CartVM = new CartViewModel()
      {
        OrderHeader = new OrderHeader(),
        CommodityList = new List<Commodity>(),
      };
    }

    public async Task<IActionResult> Index()
    {
      if (HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart) != null)
      {
        List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
        foreach (int CommodityId in sessionList)
        {
          CartVM.CommodityList.Add(await _unitOfWork.Commodity.GetFirstOrDefaultAsync(u => u.Id == CommodityId, includeProperties: "Category"));
          CartVM.SupplierList = _unitOfWork.Supplier.GetSupplierListForDropDown();
          CartVM.PaymentList = _unitOfWork.Payment.GetPaymentListForDropDown();
          CartVM.DeliveryList = _unitOfWork.Delivery.GetDeliveryListForDropDown();
        }
      }
      return View("Index", CartVM);
    }

    public async Task<IActionResult> Summary()
    {
      if (HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart) != null)
      {
        List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
        foreach (int CommodityId in sessionList)
        {
          CartVM.CommodityList.Add(await _unitOfWork.Commodity.GetFirstOrDefaultAsync(u => u.Id == CommodityId, includeProperties: "Category"));
          CartVM.SupplierList = _unitOfWork.Supplier.GetSupplierListForDropDown();
          CartVM.PaymentList = _unitOfWork.Payment.GetPaymentListForDropDown();
          CartVM.DeliveryList = _unitOfWork.Delivery.GetDeliveryListForDropDown();
          CartVM.Amount = (await _unitOfWork.Commodity.GetFirstOrDefaultAsync(u => u.Id == CommodityId, includeProperties: "Category")).Amount;
        }
        await _unitOfWork.SaveAsync();
      }
      return View(CartVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Summary")]
    public async Task<IActionResult> SummaryPOST()
    {
      if (HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart) != null)
      {
        List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
        CartVM.CommodityList = new List<Commodity>();
        foreach (int CommodityId in sessionList)
        {
          CartVM.CommodityList.Add(await _unitOfWork.Commodity.GetAsync(CommodityId));
          CartVM.SupplierList = _unitOfWork.Supplier.GetSupplierListForDropDown();
          CartVM.PaymentList = _unitOfWork.Payment.GetPaymentListForDropDown();
          CartVM.DeliveryList = _unitOfWork.Delivery.GetDeliveryListForDropDown();
          CartVM.Amount = (await _unitOfWork.Commodity.GetFirstOrDefaultAsync(u => u.Id == CommodityId, includeProperties: "Category")).Amount;
        }
      }

      if (!ModelState.IsValid)
      {
        return View(CartVM);
      }
      else
      {
        CartVM.OrderHeader.OrderDate = DateTime.Now;
        CartVM.OrderHeader.Status = UsefulConsts.StatusSubmitted;
        await _unitOfWork.OrderHeader.AddAsync(CartVM.OrderHeader);
        await _unitOfWork.SaveAsync();

        foreach (var item in CartVM.CommodityList)
        {
          OrderDetails orderDetails = new()
          {
            CommodityId = item.Id,
            OrderHeaderId = CartVM.OrderHeader.Id,
            CommodityName = item.Name,
            Price = item.Price
          };

          await _unitOfWork.OrderDetails.AddAsync(orderDetails);

        }
        await _unitOfWork.SaveAsync();
        HttpContext.Session.SetObject(UsefulConsts.SessionCart, new List<int>());
        return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.OrderHeader.Id });
      }
    }

    public IActionResult OrderConfirmation(int id)
    {
      return View(id);
    }

    public IActionResult Remove(int CommodityId)
    {
      List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
      sessionList.Remove(CommodityId);
      HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);

      return RedirectToAction(nameof(Index));
    }
  }
}