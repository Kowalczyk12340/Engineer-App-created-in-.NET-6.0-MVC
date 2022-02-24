using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Extensions;
using EngineerApplication.Entities;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;

namespace EngineerApplication.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private HomeViewModel HomeVM;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM = new HomeViewModel()
            {
                CategoryList = _unitOfWork.Category.GetAll(),
                ServiceList = _unitOfWork.Service.GetAll(includeProperties: "Frequency")
            };

            return View(HomeVM);
        }

        public IActionResult Details(int id)
        {
            var serviceFromDb = _unitOfWork.Service.GetFirstOrDefault(includeProperties: "Category,Frequency", filter: c => c.Id == id);
            return View(serviceFromDb);
        }


        public IActionResult AddToCart(int serviceId)
        {
            List<int> sessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(UsefulConsts.SessionCart)))
            {
                sessionList.Add(serviceId);
                HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
                if (!sessionList.Contains(serviceId))
                {
                    sessionList.Add(serviceId);
                    HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);
                }
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
