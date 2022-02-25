using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Authorize]
  [Area("Admin")]
  public class SupplierController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public SupplierController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Upsert(int? id)
    {
      Supplier supplier = new();

      if (id is null)
      {
        return View(supplier);
      }

      supplier = _unitOfWork.Supplier.Get(id.GetValueOrDefault());

      if (supplier is null)
      {
        return NotFound();
      }

      return View(supplier);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Supplier supplier)
    {
      if (ModelState.IsValid)
      {
        if (supplier.Id == 0)
        {
          _unitOfWork.Supplier.Add(supplier);
        }
        else
        {
          _unitOfWork.Supplier.Update(supplier);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      return View(supplier);
    }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
      return Json(new { data = _unitOfWork.Supplier.GetAll() });
      //return Json(new { data = _unitOfWork.SP_Call.ReturnList<Supplier>(UsefulConsts.usp_GetAllSupplier,null)  });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var objFromDb = _unitOfWork.Supplier.Get(id);
      if (objFromDb == null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Supplier.Remove(objFromDb);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete successful." });

    }
    #endregion
  }
}
