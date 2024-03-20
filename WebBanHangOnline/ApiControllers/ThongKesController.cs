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
    public class ThongKesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ThongKes
        public IQueryable<ThongKe> GetThongKes()
        {
            return db.ThongKes;
        }

        // GET: api/ThongKes/5
        [ResponseType(typeof(ThongKe))]
        public IHttpActionResult GetThongKe(int id)
        {
            ThongKe thongKe = db.ThongKes.Find(id);
            if (thongKe == null)
            {
                return NotFound();
            }

            return Ok(thongKe);
        }

        // PUT: api/ThongKes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThongKe(int id, ThongKe thongKe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != thongKe.Id)
            {
                return BadRequest();
            }

            db.Entry(thongKe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongKeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ThongKes
        [ResponseType(typeof(ThongKe))]
        public IHttpActionResult PostThongKe(ThongKe thongKe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ThongKes.Add(thongKe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = thongKe.Id }, thongKe);
        }

        // DELETE: api/ThongKes/5
        [ResponseType(typeof(ThongKe))]
        public IHttpActionResult DeleteThongKe(int id)
        {
            ThongKe thongKe = db.ThongKes.Find(id);
            if (thongKe == null)
            {
                return NotFound();
            }

            db.ThongKes.Remove(thongKe);
            db.SaveChanges();

            return Ok(thongKe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThongKeExists(int id)
        {
            return db.ThongKes.Count(e => e.Id == id) > 0;
        }
    }
}