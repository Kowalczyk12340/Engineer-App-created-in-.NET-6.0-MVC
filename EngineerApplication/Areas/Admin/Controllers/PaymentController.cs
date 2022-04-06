using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;

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
      return View();
    }

    public IActionResult Upsert(int? id)
    {
      Payment payment = new();
      if (id == null)
      {

        return View(payment);
      }

      payment = _unitOfWork.Payment.Get(id.GetValueOrDefault());
      if (payment == null)
      {
        return NotFound();
      }
      return View(payment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Payment payment)
    {

      if (ModelState.IsValid)
      {
        if (payment.Id == 0)
        {
          _unitOfWork.Payment.Add(payment);
        }
        else
        {
          _unitOfWork.Payment.Update(payment);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      return View(payment);

    }
    #region API Calls

    public IActionResult GetAll()
    {

      return Json(new { data = _unitOfWork.Payment.GetAll() });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var objFromDb = _unitOfWork.Payment.Get(id);
      if (objFromDb == null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }
      _unitOfWork.Payment.Remove(objFromDb);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete success." });

    }
    #endregion
  }
}