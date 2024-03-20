using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using PagedList;
using System.Globalization;
using System.Data.Entity;
using WebBanHangOnline.Models.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using WebBanHangOnline.Models.EF;
using System.Text;
using WebBanHangOnline.Controllers;
using WebBanHangOnline.Service;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private OrderService _orderService;
        public OrderController()
        {
            _orderService = new OrderService();
        }

        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var items = _orderService.GetOrders().OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult View(int id)
        {
            var item = _orderService.GetOrder(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            List<OrderDetail> orderDetailsList = _orderService.GetOrderDetails(id);
            return PartialView(orderDetailsList);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = _orderService.GetOrder(id);
            if (item != null)
            {
                item.Status = trangthai;
                HttpResponseMessage response = _orderService.PutOrders(item);
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });
        }
    }
}