using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Service;

namespace WebBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private CategoriesService _categoriesService;
        private ProductCategoryService _productCategoryService;
        public MenuController()
        {
            _categoriesService = new CategoriesService();
            _productCategoryService = new ProductCategoryService();
        }
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var items = _categoriesService.GetCategories().OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }

        public ActionResult MenuProductCategory()
        {
            var items = _productCategoryService.GetProductCategories();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = _productCategoryService.GetProductCategories();
            return PartialView("_MenuLeft", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = _productCategoryService.GetProductCategories();
            return PartialView("_MenuArrivals", items);
        }

    }
}