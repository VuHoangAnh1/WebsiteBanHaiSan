using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Service;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductImageController : Controller
    {
        private ProductImageService _service;
        public ProductImageController()
        {
            _service = new ProductImageService();
        }
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            List<ProductImage> productImagesList = _service.GetProductImagesByProductId(id);
            return View(productImagesList);
        }

        [HttpPost]
        public ActionResult AddImage(int productId,string url)
        {
            var model = new ProductImage { 
                ProductId=productId,
                Image=url,
                IsDefault=false
            };
            HttpResponseMessage response = _service.PostProductImage(model);
            return Json(new { Success=true});
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = _service.DeleteProductImage(id);
            if(response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}