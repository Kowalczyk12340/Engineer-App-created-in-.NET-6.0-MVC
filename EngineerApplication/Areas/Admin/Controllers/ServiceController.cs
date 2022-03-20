﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;

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
      return View();
    }

    public IActionResult Upsert(int? id)
    {
      ServiceVM = new ServiceVM()
      {
        Service = new Entities.Service(),
        CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
        SupplierList = _unitOfWork.Supplier.GetSupplierListForDropDown(),
        FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),
        DeliveryList = _unitOfWork.Delivery.GetDeliveryListForDropDown(),
      };
      if (id != null)
      {
        ServiceVM.Service = _unitOfWork.Service.Get(id.GetValueOrDefault());
      }

      return View(ServiceVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert()
    {
      if (ModelState.IsValid)
      {
        string webRootPath = _hostEnvironment.WebRootPath;
        var files = HttpContext.Request.Form.Files;
        if (ServiceVM.Service.Id == 0)
        {
          //New Service
          string fileName = Guid.NewGuid().ToString();
          var uploads = Path.Combine(webRootPath, @"images\Services");
          var extension = Path.GetExtension(files[0].FileName);

          using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
          {
            files[0].CopyTo(fileStreams);
          }
          ServiceVM.Service.ImageUrl = @"\images\Services\" + fileName + extension;

          _unitOfWork.Service.Add(ServiceVM.Service);
        }
        else
        {
          //Edit Service
          var ServiceFromDb = _unitOfWork.Service.Get(ServiceVM.Service.Id);
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

          _unitOfWork.Service.Update(ServiceVM.Service);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        ServiceVM.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
        ServiceVM.SupplierList = _unitOfWork.Supplier.GetSupplierListForDropDown();
        ServiceVM.FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown();
        ServiceVM.DeliveryList = _unitOfWork.Delivery.GetDeliveryListForDropDown();
        return View(ServiceVM);
      }
    }
    #region API Calls
    public IActionResult GetAll()
    {
      return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Category,Supplier,Frequency,Delivery") });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var ServiceFromDb = _unitOfWork.Service.Get(id);
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
      _unitOfWork.Save();
      return Json(new { success = true, message = "Deleted Successfully." });
    }
    #endregion
  }
}