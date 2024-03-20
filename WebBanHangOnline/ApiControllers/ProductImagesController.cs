using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.ApiControllers
{
    public class ProductImagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProductImages
        public IQueryable<ProductImage> GetProductImages()
        {
            return db.ProductImages;
        }

        // GET: api/ProductImages/5
        [ResponseType(typeof(ProductImage))]
        public IHttpActionResult GetProductImage(int id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return NotFound();
            }

            return Ok(productImage);
        }

        [ResponseType(typeof(ProductImage))]
        [Route("api/productImages/{productId}/GetProductImageByProductId")]
        public IHttpActionResult GetProductImageByProductId(int productId)
        {
            List<ProductImage> productImages = db.ProductImages.Where(c => c.ProductId.Equals(productId)).ToList();
            if (productImages == null)
            {
                return NotFound();
            }

            return Ok(productImages);
        }

        // POST: api/ProductImages
        [ResponseType(typeof(ProductImage))]
        public IHttpActionResult PostProductImage(ProductImage productImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductImages.Add(productImage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productImage.Id }, productImage);
        }

        // DELETE: api/ProductImages/5
        [ResponseType(typeof(ProductImage))]
        public IHttpActionResult DeleteProductImage(int id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return NotFound();
            }

            db.ProductImages.Remove(productImage);
            db.SaveChanges();

            return Ok(productImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductImageExists(int id)
        {
            return db.ProductImages.Count(e => e.Id == id) > 0;
        }
    }
}