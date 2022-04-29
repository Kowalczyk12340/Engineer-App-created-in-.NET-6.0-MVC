using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using iText.Html2pdf;

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

    public async Task<IActionResult> Upsert(int? id)
    {
      Supplier supplier = new();

      if (id is null)
      {
        return View(supplier);
      }

      supplier = await _unitOfWork.Supplier.GetAsync(id.GetValueOrDefault());

      if (supplier is null)
      {
        return NotFound();
      }

      return View(supplier);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Supplier supplier)
    {
      if (ModelState.IsValid)
      {
        if (supplier.Id == 0)
        {
          await _unitOfWork.Supplier.AddAsync(supplier);
        }
        else
        {
          await _unitOfWork.Supplier.UpdateAsync(supplier);
        }

        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }

      return View(supplier);
    }

    [HttpPost("exportSupplier")]
    public IActionResult Export(string GridHtml)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        HtmlConverter.ConvertToPdf(GridHtml, stream);
        return File(stream.ToArray(), "application/pdf", $"SupplierData_{DateTime.UtcNow}.pdf", true);
      }
    }

    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _unitOfWork.Supplier.GetAllAsync() });
      //return Json(new { data = _unitOfWork.SP_Call.ReturnList<Supplier>(UsefulConsts.usp_GetAllSupplier,null)  });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var objFromDb = await _unitOfWork.Supplier.GetAsync(id);

      if (objFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Supplier.Remove(objFromDb);
      await _unitOfWork.SaveAsync();

      return Json(new { success = true, message = "Delete successful." });
    }
    #endregion
  }
}
