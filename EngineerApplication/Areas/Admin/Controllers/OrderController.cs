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

    public IActionResult Details(int id)
    {
      OrderViewModel orderVM = new()
      {
        OrderHeader = _unitOfWork.OrderHeader.Get(id),
        OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id)
      };
      return View(orderVM);
    }

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