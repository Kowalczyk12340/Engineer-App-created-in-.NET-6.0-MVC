using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
      return View();
    }


    public IActionResult Upsert(int? id)
    {
      Delivery delivery = new();
      if (id == null)
      {
        return View(delivery);
      }
      delivery = _unitOfWork.Delivery.Get(id.GetValueOrDefault());
      if (delivery == null)
      {
        return NotFound();
      }
      return View(delivery);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Delivery delivery)
    {
      if (ModelState.IsValid)
      {
        if (delivery.Id == 0)
        {
          _unitOfWork.Delivery.Add(delivery);
        }
        else
        {
          _unitOfWork.Delivery.Update(delivery);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      return View(delivery);
    }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
      return Json(new { data = _unitOfWork.Delivery.GetAll() });
      //return Json(new { data = _unitOfWork.SP_Call.ReturnList<Delivery>(UsefulConsts.usp_GetAllDelivery,null)  });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var objFromDb = _unitOfWork.Delivery.Get(id);
      if (objFromDb == null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Delivery.Remove(objFromDb);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete successful." });

    }
    #endregion
  }
}
