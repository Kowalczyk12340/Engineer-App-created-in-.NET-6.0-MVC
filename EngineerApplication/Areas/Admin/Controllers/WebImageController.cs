#nullable disable
using EngineerApplication.ContextStructure.Data;
using EngineerApplication.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EngineerApplication.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class WebImageController : Controller
  {
    private readonly EngineerDbContext _db;

    public WebImageController(EngineerDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      return View("Index");
    }


    [Authorize]
    public async Task<IActionResult> AddOrUpdate(int? id)
    {
      WebImages imageObj = new();

      if (id is null)
#pragma warning disable S108 // Nested blocks of code should not be left empty
      {
      }
#pragma warning restore S108 // Nested blocks of code should not be left empty
      else
      {
        imageObj = await _db.WebImages.SingleOrDefaultAsync(m => m.Id == id);

        if (imageObj is null)
        {
          return NotFound();
        }

      }
      return View(imageObj);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdate(int id, WebImages imageObj)
    {
      if (ModelState.IsValid)
      {
        var files = HttpContext.Request.Form.Files;
        if (files.Count > 0)
        {
          byte[] p1 = null;
          using (var fs1 = files[0].OpenReadStream())
          {
            using var ms1 = new MemoryStream();
            await fs1.CopyToAsync(ms1);
            p1 = ms1.ToArray();
          }
          imageObj.Picture = p1;
        }

        if (imageObj.Id == 0)
        {
          await _db.WebImages.AddAsync(imageObj);
        }
        else
        {
          var imageFromDb = await _db.WebImages.Where(c => c.Id == id).FirstOrDefaultAsync();

          imageFromDb.Name = imageObj.Name;
          if (files.Count > 0)
          {
            imageFromDb.Picture = imageObj.Picture;
          }
        }

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      return View(imageObj);
    }
    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Json(new { data = await _db.WebImages.ToListAsync() });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var objFromDb = await _db.WebImages.FindAsync(id);
      if (objFromDb is null)
      {
        return Json(new { success = false, message = "Error while deleting." });
      }

      _db.WebImages.Remove(objFromDb);
      await _db.SaveChangesAsync();
      return Json(new { success = true, message = "Delete successful." });

    }
    #endregion
  }
}