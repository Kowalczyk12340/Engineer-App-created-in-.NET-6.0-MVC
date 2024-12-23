﻿using iText.Html2pdf;
using Microsoft.AspNetCore.Mvc;

namespace EngineerApplication.Areas.Customer.Controllers
{
  [Area("Customer")]
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost("exportHome")]
    public IActionResult Export(string GridHtml)
    {
      using MemoryStream stream = new();
      HtmlConverter.ConvertToPdf(GridHtml, stream);
      return File(stream.ToArray(), "application/pdf", $"HomeData_{DateTime.Now}.pdf", true);
    }
  }
}
