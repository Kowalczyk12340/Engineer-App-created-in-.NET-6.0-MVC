using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize]
  public class OrderController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
      return View();
    }

    [Authorize]
    public IActionResult Details(int id)
    {
      OrderViewModel orderVM = new()
      {
        OrderHeader = _unitOfWork.OrderHeader.Get(id),
        OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id),
      };
      orderVM.TimeToOrder = _unitOfWork.OrderHeader.Get(id).TimeToOrder.ToUniversalTime();
      orderVM.TimeToOrder = _unitOfWork.OrderHeader.Get(id).TimeToRealisation.ToUniversalTime();
      orderVM.Supplier = _unitOfWork.Supplier.GetFirstOrDefault(filter: o => o.Id == orderVM.OrderHeader.SupplierId);
      orderVM.Delivery = _unitOfWork.Delivery.GetFirstOrDefault(filter: o => o.Id == orderVM.OrderHeader.DeliveryId);
      orderVM.Payment = _unitOfWork.Payment.GetFirstOrDefault(filter: o => o.Id == orderVM.OrderHeader.PaymentId);
      _unitOfWork.Save();
      return View(orderVM);
    }

    [Authorize(Roles = UsefulConsts.Admin)]
    public IActionResult Approve(int id)
    {
      var orderFromDb = _unitOfWork.OrderHeader.Get(id);
      if (orderFromDb == null)
      {
        return NotFound();
      }
      _unitOfWork.OrderHeader.ChangeOrderStatus(id, UsefulConsts.StatusApproved);
      return View(nameof(Index));
    }

    [Authorize(Roles = UsefulConsts.Admin)]
    public IActionResult Reject(int id)
    {
      var orderFromDb = _unitOfWork.OrderHeader.Get(id);
      if (orderFromDb == null)
      {
        return NotFound();
      }
      _unitOfWork.OrderHeader.ChangeOrderStatus(id, UsefulConsts.StatusRejected);
      return View(nameof(Index));
    }
    #region API Calls

    public IActionResult GetAllOrders()
    {
      return Json(new { data = _unitOfWork.OrderHeader.GetAll() });
    }

    public IActionResult GetAllPendingOrders()
    {
      return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == UsefulConsts.StatusSubmitted) });
    }
    public IActionResult GetAllApprovedOrders()
    {
      return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == UsefulConsts.StatusApproved) });
    }

    #endregion
  }
}