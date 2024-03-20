using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Service;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin,Employee")]
    public class ProductsController : Controller
    {
        Uri BaseAddress = new Uri("https://localhost:44375/api");
        private readonly HttpClient _client;
        private ProductService _productService;
        private ProductCategoryService _productCategoryService;
        public ProductsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = BaseAddress;
            _productService = new ProductService();
            _productCategoryService = new ProductCategoryService();
        }
       

        List<ProductCategory> GetProductCategories()
        {
            List<ProductCategory> productCategoriesList = _productCategoryService.GetProductCategories();
            return productCategoriesList;
        }
        // GET: Admin/Products
        public ActionResult Index(int? page)
        {
            IEnumerable<Product> items = _productService.GetProducts().OrderByDescending(c=>c.Id);
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(_productCategoryService
                                                    .GetProductCategories().ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                if (string.IsNullOrEmpty(model.Alias))
                    model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                HttpResponseMessage response = _productService.PostProduct(model);
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_productCategoryService.GetProductCategories().ToList(), "Id", "Title");
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(_productCategoryService.GetProductCategories().ToList(), "Id", "Title");
            Product product = _productService.GetProduct(id);   

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                var response = _productService.PutProduct(model);
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _productService.GetProduct(id);
            if (item != null)
            {
                var checkImg = item.ProductImage.Where(x => x.ProductId == item.Id);
                if (checkImg != null)
                {
                    foreach(var img in checkImg)
                    {
                        HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress + "/ProductImages/" + img.Id).Result;
                    }
                }
                HttpResponseMessage response = _productService.DeleteProduct(id);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _productService.GetProduct(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _productService.PutProduct(item);
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = _productService.GetProduct(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                _productService.PutProduct(item);
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = _productService.GetProduct(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                _productService.PutProduct(item);
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }
    }
}