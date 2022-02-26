using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;

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

    public IActionResult Upsert(int? id)
    {
      CommodityVM = new CommodityVM()
      {
        Commodity = new Entities.Commodity(),
        CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
        SupplierList = _unitOfWork.Supplier.GetSupplierListForDropDown(),
        FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),
      };
      if (id != null)
      {
        CommodityVM.Commodity = _unitOfWork.Commodity.Get(id.GetValueOrDefault());
      }

      return View(CommodityVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert()
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

          _unitOfWork.Commodity.Add(CommodityVM.Commodity);
        }
        else
        {
          //Edit Commodity
          var CommodityFromDb = _unitOfWork.Commodity.Get(CommodityVM.Commodity.Id);
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

          _unitOfWork.Commodity.Update(CommodityVM.Commodity);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        CommodityVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
        CommodityVM.SupplierList = _unitOfWork.Supplier.GetSupplierListForDropDown();
        CommodityVM.FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown();
        return View(CommodityVM);
      }
    }
    #region API Calls
    public IActionResult GetAll()
    {
      return Json(new { data = _unitOfWork.Commodity.GetAll(includeProperties: "Category,Supplier,Frequency") });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var CommodityFromDb = _unitOfWork.Commodity.Get(id);
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
      _unitOfWork.Save();
      return Json(new { success = true, message = "Deleted Successfully." });
    }
    #endregion
  }
}