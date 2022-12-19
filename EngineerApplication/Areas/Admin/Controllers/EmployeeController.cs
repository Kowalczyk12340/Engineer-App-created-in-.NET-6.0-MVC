#nullable disable
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
      return View("Index");
    }

    public async Task<IActionResult> AddOrUpdate(int? id)
    {
      EmployeeVM = new EmployeeVM()
      {
        Employee = new Entities.Employee(),
        ServiceList = _unitOfWork.Service.GetServiceListForDropDown(),
      };

      if (id != null)
      {
        EmployeeVM.Employee = await _unitOfWork.Employee.GetAsync(id.GetValueOrDefault());
      }

      return View(EmployeeVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdate()
    {
      if (ModelState.IsValid)
      {
        if (EmployeeVM.Employee.Id == 0)
        {
          await _unitOfWork.Employee.AddAsync(EmployeeVM.Employee);
        }
        else
        {
          var EmployeeFromDb = await _unitOfWork.Employee.GetAsync(EmployeeVM.Employee.Id);
          await _unitOfWork.Employee.UpdateAsync(EmployeeVM.Employee);
        }
        await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        EmployeeVM.ServiceList = _unitOfWork.Service.GetServiceListForDropDown();
        return View(EmployeeVM);
      }
    }

    [HttpPost("exportEmployee")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"EmployeeData_{DateTime.Now}.pdf", true);
    }

    #region API Calls
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _unitOfWork.Employee.GetAllAsync(includeProperties: "Service") });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var EmployeeFromDb = await _unitOfWork.Employee.GetAsync(id);

      if (EmployeeFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Employee.Remove(EmployeeFromDb);
      await _unitOfWork.SaveAsync();
      return Json(new { success = true, message = "Usunięto prawdiłowo." });
    }
    #endregion
  }
}