using Microsoft.AspNetCore.Mvc;
using EngineerApplication.Extensions;
using EngineerApplication.Entities;
using EngineerApplication.Entities.ViewModels;
using EngineerApplication.Helpers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.Areas.Employee.Controllers
{
    [Area("Employee")]
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
                CommodityList = new List<Commodity>()
            };
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart)!=null)
            {
                List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
                foreach(int CommodityId in sessionList)
                {
                    CartVM.CommodityList.Add(_unitOfWork.Commodity.GetFirstOrDefault(u => u.Id == CommodityId, includeProperties: "Frequency,Category"));
                }
            }
                return View(CartVM);
        }


        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart) != null)
            {
                List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
                foreach (int CommodityId in sessionList)
                {
                    CartVM.CommodityList.Add(_unitOfWork.Commodity.GetFirstOrDefault(u => u.Id == CommodityId, includeProperties: "Frequency,Category"));
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
                CartVM.CommodityList = new List<Commodity>();
                foreach (int CommodityId in sessionList)
                {
                    CartVM.CommodityList.Add(_unitOfWork.Commodity.Get(CommodityId));
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
                CartVM.OrderHeader.Count = CartVM.CommodityList.Count;
                _unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
                _unitOfWork.Save();

                foreach(var item in CartVM.CommodityList)
                {
                    OrderDetails orderDetails = new()
                    {
                        CommodityId = item.Id,
                        OrderHeaderId = CartVM.OrderHeader.Id,
                        CommodityName = item.Name,
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


        public IActionResult Remove(int CommodityId)
        {
            List<int> sessionList = HttpContext.Session.GetObject<List<int>>(UsefulConsts.SessionCart);
            sessionList.Remove(CommodityId);
            HttpContext.Session.SetObject(UsefulConsts.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));
        }
    }
}