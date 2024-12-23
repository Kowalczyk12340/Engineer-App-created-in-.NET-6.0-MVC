﻿#nullable disable
using System.Security.Claims;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Helpers;
using iText.Html2pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = UsefulConsts.Admin)]
  public class UserController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public UserController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
      var claimsIdentity = (ClaimsIdentity)User.Identity;
      var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

      return View("Index", await _unitOfWork.User.GetAllAsync(u => u.Id != claims.Value));
    }

    public async Task<IActionResult> Lock(string id)
    {
      if (id is null)
      {
        return NotFound();
      }

      await _unitOfWork.User.LockUserAsync(id);

      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UnLock(string id)
    {
      if (id is null)
      {
        return NotFound();
      }

      await _unitOfWork.User.UnLockUserAsync(id);

      return RedirectToAction(nameof(Index));
    }
  }
}