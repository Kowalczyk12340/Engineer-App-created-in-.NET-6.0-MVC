using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EngineerApplication.ContextStructure.Data.Repository;
using EngineerApplication.ContextStructure.Data.Repository.IRepository;
using EngineerApplication.Extensions;
using EngineerApplication.Entities;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;

namespace EngineerApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CartViewModel CartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartVM = new CartViewModel()
            {
                OrderHeader = new Entities.OrderHeader(),
                ServiceList = new List<Service>()
            };
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart)!=null)
            {
                List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
                foreach(int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }
                return View(CartVM);
        }


        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart) != null)
            {
                List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
                foreach (int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }
            return View(CartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            if (HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart) != null)
            {
                List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
                CartVM.ServiceList = new List<Service>();
                foreach (int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfWork.Service.Get(serviceId));
                }
            }

            if (!ModelState.IsValid)
            {
                return View(CartVM);
            }
            else
            {
                CartVM.OrderHeader.OrderDate = DateTime.Now;
                CartVM.OrderHeader.Status = UsefulConsts.StatusSubmitted;
                CartVM.OrderHeader.ServiceCount = CartVM.ServiceList.Count;
                _unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
                _unitOfWork.Save();

                foreach(var item in CartVM.ServiceList)
                {
                    OrderDetails orderDetails = new()
                    {
                        ServiceId = item.Id,
                        OrderHeaderId = CartVM.OrderHeader.Id,
                        ServiceName = item.Name,
                        Price = item.Price
                    };

                    _unitOfWork.OrderDetails.Add(orderDetails);

                }
                _unitOfWork.Save();
                HttpContext.Session.SetObject(UsefulConsts.SessionCart, new List<int>());
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.OrderHeader.Id });
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }


        public IActionResult Remove(int serviceId)
        {
            List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
            sessionList.Remove(serviceId);
            HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));
        }
    }
}