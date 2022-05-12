using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;
using iText.Html2pdf;

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
    public async Task<IActionResult> Details(int id)
    {
      OrderViewModel orderVM = new()
      {
        OrderHeader = await _unitOfWork.OrderHeader.GetAsync(id),
        OrderDetails = await _unitOfWork.OrderDetails.GetAllAsync(filter: o => o.OrderHeaderId == id),
      };
      orderVM.TimeToOrder = (await _unitOfWork.OrderHeader.GetAsync(id)).TimeToOrder.ToUniversalTime();
      orderVM.TimeToOrder = (await _unitOfWork.OrderHeader.GetAsync(id)).TimeToRealisation.ToUniversalTime();
      orderVM.Supplier = await _unitOfWork.Supplier.GetFirstOrDefaultAsync(filter: o => o.Id == orderVM.OrderHeader.SupplierId);
      orderVM.Delivery = await _unitOfWork.Delivery.GetFirstOrDefaultAsync(filter: o => o.Id == orderVM.OrderHeader.DeliveryId);
      orderVM.Payment = await _unitOfWork.Payment.GetFirstOrDefaultAsync(filter: o => o.Id == orderVM.OrderHeader.PaymentId);
      await _unitOfWork.SaveAsync();
      return View(orderVM);
    }

    [Authorize(Roles = UsefulConsts.Admin)]
    public async Task<IActionResult> Approve(int id)
    {
      var orderFromDb = await _unitOfWork.OrderHeader.GetAsync(id);

      if (orderFromDb is null)
      {
        return NotFound();
      }
      await _unitOfWork.OrderHeader.ChangeOrderStatusAsync(id, UsefulConsts.StatusApproved);
      return View(nameof(Index));
    }

    [Authorize(Roles = UsefulConsts.Admin)]
    public async Task<IActionResult> Reject(int id)
    {
      var orderFromDb = await _unitOfWork.OrderHeader.GetAsync(id);

      if (orderFromDb is null)
      {
        return NotFound();
      }

      await _unitOfWork.OrderHeader.ChangeOrderStatusAsync(id, UsefulConsts.StatusRejected);
      return View(nameof(Index));
    }

    [HttpPost("exportOrder")]
    public IActionResult Export(string GridHtml)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        HtmlConverter.ConvertToPdf(GridHtml, stream);
        return File(stream.ToArray(), "application/pdf", $"OrderData_{DateTime.UtcNow}.pdf", true);
      }
    }
    #region API Calls

    public async Task<IActionResult> GetAllOrders()
    {
      return Json(new { data = await _unitOfWork.OrderHeader.GetAllAsync() });
    }

    public async Task<IActionResult> GetAllPendingOrders()
    {
      return Json(new { data = await _unitOfWork.OrderHeader.GetAllAsync(filter: o => o.Status == UsefulConsts.StatusSubmitted) });
    }

    public async Task<IActionResult> GetAllApprovedOrders()
    {
      return Json(new { data = await _unitOfWork.OrderHeader.GetAllAsync(filter: o => o.Status == UsefulConsts.StatusApproved) });
    }
    #endregion
  }
}