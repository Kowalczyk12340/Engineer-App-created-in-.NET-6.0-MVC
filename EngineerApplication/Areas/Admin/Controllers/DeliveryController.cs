using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using iText.Html2pdf;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize]
  public class DeliveryController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public DeliveryController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      return View("Index");
    }

    public async Task<IActionResult> AddOrUpdate(int? id)
    {
      Delivery delivery = new();
      if (id is null)
      {
        return View(delivery);
      }

      delivery = await _unitOfWork.Delivery.GetAsync(id.GetValueOrDefault());

      if (delivery is null)
      {
        return NotFound();
      }

      return View(delivery);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdate(Delivery delivery)
    {
      if (ModelState.IsValid)
      {
        if (delivery.Id == 0)
        {
          await _unitOfWork.Delivery.AddAsync(delivery);
        }
        else
        {
          await _unitOfWork.Delivery.UpdateAsync(delivery);
        }

        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(delivery);
    }

    [HttpPost("exportDelivery")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"DeliveryData_{DateTime.UtcNow}.pdf", true);
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _unitOfWork.Delivery.GetAllAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var objFromDb = await _unitOfWork.Delivery.GetAsync(id);
      if (objFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Delivery.Remove(objFromDb);
      await _unitOfWork.SaveAsync();
      return Json(new { success = true, message = "Delete successful." });
    }
    #endregion
  }
}
