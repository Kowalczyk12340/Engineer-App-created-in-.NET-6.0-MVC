#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;
using iText.Html2pdf;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize]
  public class ServiceController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    [BindProperty]
    public ServiceVM ServiceVM { get; set; }

    public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
    {
      _unitOfWork = unitOfWork;
      _hostEnvironment = hostEnvironment;
    }

    public IActionResult Index()
    {
      return View("Index");
    }

    public async Task<IActionResult> Upsert(int? id)
    {
      ServiceVM = new ServiceVM()
      {
        Service = new Entities.Service(),
        CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
        PaymentList = _unitOfWork.Payment.GetPaymentListForDropDown(),
      };

      if (id != null)
      {
        ServiceVM.Service = await _unitOfWork.Service.GetAsync(id.GetValueOrDefault());
      }

      return View(ServiceVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert()
    {
      if (ModelState.IsValid)
      {
        string webRootPath = _hostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (ServiceVM.Service.Id == 0)
        {
          string fileName = Guid.NewGuid().ToString();
          var uploads = Path.Combine(webRootPath, @"images\Services");
          var extension = Path.GetExtension(files[0].FileName);

          using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
          {
            files[0].CopyTo(fileStreams);
          }
          ServiceVM.Service.ImageUrl = @"\images\Services\" + fileName + extension;

          await _unitOfWork.Service.AddAsync(ServiceVM.Service);
        }
        else
        {
          //Edit Service
          var ServiceFromDb = await _unitOfWork.Service.GetAsync(ServiceVM.Service.Id);
          if (files.Count > 0)
          {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, @"images\Services");
            var extension_new = Path.GetExtension(files[0].FileName);

            var imagePath = Path.Combine(webRootPath, ServiceFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
              System.IO.File.Delete(imagePath);
            }

            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
            {
              files[0].CopyTo(fileStreams);
            }
            ServiceVM.Service.ImageUrl = @"\images\Services\" + fileName + extension_new;
          }
          else
          {
            ServiceVM.Service.ImageUrl = ServiceFromDb.ImageUrl;
          }

          await _unitOfWork.Service.UpdateAsync(ServiceVM.Service);
        }
        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        ServiceVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
        ServiceVM.PaymentList = _unitOfWork.Payment.GetPaymentListForDropDown();
        return View(ServiceVM);
      }
    }

    [HttpPost("exportService")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"ServiceData_{DateTime.UtcNow}.pdf", true);
    }

    #region API Calls
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _unitOfWork.Service.GetAllAsync(includeProperties: "Category,Payment") });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var ServiceFromDb = await _unitOfWork.Service.GetAsync(id);
      string webRootPath = _hostEnvironment.WebRootPath;
      var imagePath = Path.Combine(webRootPath, ServiceFromDb.ImageUrl.TrimStart('\\'));
      if (System.IO.File.Exists(imagePath))
      {
        System.IO.File.Delete(imagePath);
      }

      if (ServiceFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Service.Remove(ServiceFromDb);
      await _unitOfWork.SaveAsync();
      return Json(new { success = true, message = "Deleted Successfully." });
    }
    #endregion
  }
}