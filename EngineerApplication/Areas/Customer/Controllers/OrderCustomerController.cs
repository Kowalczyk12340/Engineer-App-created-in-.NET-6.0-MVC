using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  [Authorize(Roles = UsefulConsts.Customer)]
  public class OrderCustomerController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _manager;

    public OrderCustomerController(IUnitOfWork unitOfWork, UserManager<IdentityUser> manager)
    {
      _unitOfWork = unitOfWork;
      _manager = manager;
    }

    public async Task<IActionResult> Index()
    {
      var user = await GetCurrentUser();
      string userEmail = user.Email;
      var orderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(x => x.Email.Equals(userEmail));
      return View("Index", orderHeaders);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
      OrderViewModel orderVM = new()
      {
        OrderHeader = await _unitOfWork.OrderHeader.GetAsync(id),
        OrderDetails = await _unitOfWork.OrderDetails.GetAllAsync(filter: o => o.OrderHeaderId == id),
      };
      orderVM.TimeToOrder = (await _unitOfWork.OrderHeader.GetAsync(id)).TimeToOrder;
      orderVM.TimeToRealisation = (await _unitOfWork.OrderHeader.GetAsync(id)).TimeToRealisation;
      orderVM.Supplier = await _unitOfWork.Supplier.GetFirstOrDefaultAsync(filter: o => o.Id == orderVM.OrderHeader.SupplierId);
      orderVM.Delivery = await _unitOfWork.Delivery.GetFirstOrDefaultAsync(filter: o => o.Id == orderVM.OrderHeader.DeliveryId);
      orderVM.Payment = await _unitOfWork.Payment.GetFirstOrDefaultAsync(filter: o => o.Id == orderVM.OrderHeader.PaymentId);
      await _unitOfWork.SaveAsync();
      return View(orderVM);
    }

    [HttpPost("exportOrderCustomer")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"OrderData_{DateTime.Now}.pdf", true);
    }

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

    private async Task<IdentityUser> GetCurrentUser()
    {
      return await _manager.GetUserAsync(HttpContext.User);
    }
  }
}
