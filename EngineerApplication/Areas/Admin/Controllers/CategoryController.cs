#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using iText.Html2pdf;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Authorize]
  [Area("Admin")]
  public class CategoryController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      return View("Index");
    }


    public async Task<IActionResult> Upsert(int? id)
    {
      Category category = new();
      if (id == null)
      {
        return View(category);
      }
      category = await _unitOfWork.Category.GetAsync(id.GetValueOrDefault());

      if (category == null)
      {
        return NotFound();
      }

      return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Category category)
    {
      if (ModelState.IsValid)
      {
        if (category.Id == 0)
        {
          await _unitOfWork.Category.AddAsync(category);
        }
        else
        {
          await _unitOfWork.Category.UpdateAsync(category);
        }

        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(category);
    }

    [HttpPost("exportCategory")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"CategoryData_{DateTime.UtcNow}.pdf", true);
    }

    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _unitOfWork.Category.GetAllAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var objFromDb = await _unitOfWork.Category.GetAsync(id);
      if (objFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Category.Remove(objFromDb);
      await _unitOfWork.SaveAsync();
      return Json(new { success = true, message = "Delete successful." });
    }
    #endregion
  }
}