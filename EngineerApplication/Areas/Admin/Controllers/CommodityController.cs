using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Entities;
using iText.Html2pdf;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize]
  public class CommodityController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    [BindProperty]
    public CommodityVM CommodityVM { get; set; }

    public CommodityController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
      _unitOfWork = unitOfWork;
      _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> Upsert(int? id)
    {
      CommodityVM = new CommodityVM()
      {
        Commodity = new Commodity(),
        CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
      };
      if (id != null)
      {
        CommodityVM.Commodity = await _unitOfWork.Commodity.GetAsync(id.GetValueOrDefault());
      }

      return View(CommodityVM);
    }

    [HttpPost("exportCommodity")]
    public IActionResult Export(string GridHtml)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        HtmlConverter.ConvertToPdf(GridHtml, stream);
        return File(stream.ToArray(), "application/pdf", $"CommodityData_{DateTime.UtcNow}.pdf", true);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert()
    {
      if (ModelState.IsValid)
      {
        string webRootPath = _hostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (CommodityVM.Commodity.Id == 0)
        {
          //New Commodity
          string fileName = Guid.NewGuid().ToString();
          var uploads = Path.Combine(webRootPath, @"images\Services");
          var extension = Path.GetExtension(files[0].FileName);

          using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
          {
            files[0].CopyTo(fileStreams);
          }
          CommodityVM.Commodity.ImageUrl = @"\images\Services\" + fileName + extension;

          await _unitOfWork.Commodity.AddAsync(CommodityVM.Commodity);
        }
        else
        {
          //Edit Commodity
          var CommodityFromDb = await _unitOfWork.Commodity.GetAsync(CommodityVM.Commodity.Id);
          if (files.Count > 0)
          {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, @"images\Services");
            var extension_new = Path.GetExtension(files[0].FileName);

            var imagePath = Path.Combine(webRootPath, CommodityFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
              System.IO.File.Delete(imagePath);
            }

            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
            {
              files[0].CopyTo(fileStreams);
            }
            CommodityVM.Commodity.ImageUrl = @"\images\Services\" + fileName + extension_new;
          }
          else
          {
            CommodityVM.Commodity.ImageUrl = CommodityFromDb.ImageUrl;
          }

          await _unitOfWork.Commodity.UpdateAsync(CommodityVM.Commodity);
        }
        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        CommodityVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
        return View(CommodityVM);
      }
    }
    #region API Calls
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _unitOfWork.Commodity.GetAllAsync(includeProperties: "Category") });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var CommodityFromDb = await _unitOfWork.Commodity.GetAsync(id);
      string webRootPath = _hostEnvironment.WebRootPath;
      var imagePath = Path.Combine(webRootPath, CommodityFromDb.ImageUrl.TrimStart('\\'));
      if (System.IO.File.Exists(imagePath))
      {
        System.IO.File.Delete(imagePath);
      }

      if (CommodityFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Commodity.Remove(CommodityFromDb);
      await _unitOfWork.SaveAsync();
      return Json(new { success = true, message = "Deleted Successfully." });
    }
    #endregion
  }
}