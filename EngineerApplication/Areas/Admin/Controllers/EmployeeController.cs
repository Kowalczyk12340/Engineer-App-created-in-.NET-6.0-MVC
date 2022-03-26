using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize]
  public class EmployeeController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public EmployeeVM EmployeeVM { get; set; }

    public EmployeeController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Upsert(int? id)
    {
      EmployeeVM = new EmployeeVM()
      {
        Employee = new Entities.Employee(),
        ServiceList = _unitOfWork.Service.GetServiceListForDropDown(),
      };
      if (id != null)
      {
        EmployeeVM.Employee = _unitOfWork.Employee.Get(id.GetValueOrDefault());
      }

      return View(EmployeeVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert()
    {
      if (ModelState.IsValid)
      {
        if (EmployeeVM.Employee.Id == 0)
        {
          _unitOfWork.Employee.Add(EmployeeVM.Employee);
        }
        else
        {
          var EmployeeFromDb = _unitOfWork.Employee.Get(EmployeeVM.Employee.Id);
          _unitOfWork.Employee.Update(EmployeeVM.Employee);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        EmployeeVM.ServiceList = _unitOfWork.Service.GetServiceListForDropDown();
        return View(EmployeeVM);
      }
    }
    #region API Calls
    public IActionResult GetAll()
    {
      return Json(new { data = _unitOfWork.Employee.GetAll(includeProperties: "Service") });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var EmployeeFromDb = _unitOfWork.Employee.Get(id);

      if (EmployeeFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Employee.Remove(EmployeeFromDb);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Deleted Successfully." });
    }
    #endregion
  }
}