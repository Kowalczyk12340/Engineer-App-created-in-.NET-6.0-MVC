using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize]
  public class PaymentController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public PaymentController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
      return View("Index");
    }

    [HttpPost("exportPayment")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"PaymentData_{DateTime.UtcNow}.pdf", true);
    }

    public async Task<IActionResult> AddOrUpdate(int? id)
    {
      Payment payment = new();

      if (id is null)
      {

        return View(payment);
      }

      payment = await _unitOfWork.Payment.GetAsync(id.GetValueOrDefault());
      if (payment is null)
      {
        return NotFound();
      }
      return View(payment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdate(Payment payment)
    {

      if (ModelState.IsValid)
      {
        if (payment.Id == 0)
        {
          await _unitOfWork.Payment.AddAsync(payment);
        }
        else
        {
          await _unitOfWork.Payment.UpdateAsync(payment);
        }

        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(payment);
    }
    #region API Calls

    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _unitOfWork.Payment.GetAllAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var objFromDb = await _unitOfWork.Payment.GetAsync(id);

      if (objFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }
      _unitOfWork.Payment.Remove(objFromDb);
      await _unitOfWork.SaveAsync();
      return Json(new { success = true, message = "Delete success." });
    }
    #endregion
  }
}