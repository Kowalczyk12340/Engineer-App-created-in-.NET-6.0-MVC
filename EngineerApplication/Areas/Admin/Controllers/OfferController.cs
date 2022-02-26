using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities.ViewModels;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize]
  public class OfferController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public OfferVM OfferVM { get; set; }

    public OfferController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Upsert(int? id)
    {
      OfferVM = new OfferVM()
      {
        Offer = new Entities.Offer(),
        CommodityList = _unitOfWork.Commodity.GetCommodityListForDropDown(),
      };
      if (id != null)
      {
        OfferVM.Offer = _unitOfWork.Offer.Get(id.GetValueOrDefault());
      }

      return View(OfferVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert()
    {
      if (ModelState.IsValid)
      {
        if (OfferVM.Offer.Id == 0)
        {
          _unitOfWork.Offer.Add(OfferVM.Offer);
        }
        else
        {
          var OfferFromDb = _unitOfWork.Offer.Get(OfferVM.Offer.Id);
          _unitOfWork.Offer.Update(OfferVM.Offer);
        }
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }
      else
      {
        OfferVM.CommodityList = _unitOfWork.Commodity.GetCommodityListForDropDown();
        return View(OfferVM);
      }
    }
    #region API Calls
    public IActionResult GetAll()
    {
      return Json(new { data = _unitOfWork.Offer.GetAll(includeProperties: "Commodity") });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
      var OfferFromDb = _unitOfWork.Offer.Get(id);

      if (OfferFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _unitOfWork.Offer.Remove(OfferFromDb);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Deleted Successfully." });
    }
    #endregion
  }
}