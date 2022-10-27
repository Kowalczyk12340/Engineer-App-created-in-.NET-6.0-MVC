#nullable disable
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using EngineerApplication.Entities.ViewModels;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
      return View("Index");
    }

    public async Task<IActionResult> AddOrUpdate(int? id)
    {
      CommodityVM = new CommodityVM()
      {
        Commodity = new Commodity(),
        CategoryList = _unitOfWork.Category.GetCategoryForCommodityListForDropDown(),
      };
      if (id != null)
      {
        CommodityVM.Commodity = await _unitOfWork.Commodity
          .GetAsync(id.GetValueOrDefault());
      }

      return View(CommodityVM);
    }

    [HttpPost("exportCommodity")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new MemoryStream();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"CommodityData_{DateTime.UtcNow}.pdf", true);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdate()
    {
      if (ModelState.IsValid)
      {
        string webRootPath = _hostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (CommodityVM.Commodity.Id == 0)
        {
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
          var commodityFromDb = await _unitOfWork.Commodity.GetAsync(CommodityVM.Commodity.Id);
          if (files.Count > 0)
          {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, @"images\Services");
            var extension_new = Path.GetExtension(files[0].FileName);

            var imagePath = Path.Combine(webRootPath, commodityFromDb.ImageUrl.TrimStart('\\'));
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
            CommodityVM.Commodity.ImageUrl = commodityFromDb.ImageUrl;
          }

          await _unitOfWork.Commodity.UpdateAsync(CommodityVM.Commodity);
        }
        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        CommodityVM.CategoryList = _unitOfWork.Category.GetCategoryForCommodityListForDropDown();
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
      var commodityFromDb = await _unitOfWork.Commodity.GetAsync(id);
      string webRootPath = _hostEnvironment.WebRootPath;
      var imagePath = Path.Combine(webRootPath,
        commodityFromDb.ImageUrl.TrimStart('\\'));
      if (System.IO.File.Exists(imagePath))
      {
        System.IO.File.Delete(imagePath);
      }

      if (commodityFromDb is null)
      {
        return Json(new
        {
          success = false,
          message = "Error while deleting."
        });
      }

      _unitOfWork.Commodity.Remove(commodityFromDb);
      await _unitOfWork.SaveAsync();
      return Json(new { success = true, message = "Deleted Successfully." });
    }
    #endregion
  }
}