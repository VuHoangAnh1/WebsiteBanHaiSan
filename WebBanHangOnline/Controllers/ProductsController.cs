using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Service;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ProductService _productService;
        private ProductCategoryService _productCategoryService;
        // GET: Products
        public ProductsController()
        {
            _productService = new ProductService();
            _productCategoryService = new ProductCategoryService();
        }
        public ActionResult Index()
        {
            var items = _productService.GetProducts();
            
            return View(items);
        }

        public ActionResult Detail(string alias,int id)
        {
            var item = _productService.GetProduct(id);
            if (item != null)
            {
                item.ViewCount = item.ViewCount + 1;
                _productService.PutProduct(item);
            }
            
            return View(item);
        }
        public ActionResult ProductCategory(string alias,int id)
        {
            var items = _productService.GetProducts();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = _productCategoryService.GetProductCategory(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            return View(items);
        }
        public ActionResult Partial_ItemsByCateId()
        {
            var items = _productService.GetProducts().Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = _productService.GetProducts().Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Individual_product()
        {
            var items = _productService.GetProducts().Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
    }
}