using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.ApiControllers
{
    public class SubscribesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Subscribes
        public IQueryable<Subscribe> GetSubscribes()
        {
            return db.Subscribes;
        }

        // GET: api/Subscribes/5
        [ResponseType(typeof(Subscribe))]
        public IHttpActionResult GetSubscribe(int id)
        {
            Subscribe subscribe = db.Subscribes.Find(id);
            if (subscribe == null)
            {
                return NotFound();
            }

            return Ok(subscribe);
        }

        // PUT: api/Subscribes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubscribe(Subscribe subscribe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Subscribes.AddOrUpdate(subscribe);

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Subscribes
        [ResponseType(typeof(Subscribe))]
        public IHttpActionResult PostSubscribe(Subscribe subscribe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subscribes.Add(subscribe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subscribe.Id }, subscribe);
        }

        // DELETE: api/Subscribes/5
        [ResponseType(typeof(Subscribe))]
        public IHttpActionResult DeleteSubscribe(int id)
        {
            Subscribe subscribe = db.Subscribes.Find(id);
            if (subscribe == null)
            {
                return NotFound();
            }

            db.Subscribes.Remove(subscribe);
            db.SaveChanges();

            return Ok(subscribe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubscribeExists(int id)
        {
            return db.Subscribes.Count(e => e.Id == id) > 0;
        }
    }
}