using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class EventController : Controller
    {
         EventdbEntities2 dc = new EventdbEntities2();

        // GET: Event
        public ActionResult Index()
        {
            var res = (from q in dc.Events
                      select q).Count();
            ViewBag.TotalEvent = res;

            

            return View(dc.Events.ToList());
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event event1 = dc.Events.Find(id);

            if (event1 == null)
            {
                return HttpNotFound();
            }
            return View(event1);
            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Event event1 = dc.Events.Find(id);

            var res = (from q in dc.Comments
                where q.eventid == id
                select q.commentid).SingleOrDefault();

            Comment c = dc.Comments.Find(res);

            dc.Events.Remove(event1);
            dc.Comments.Remove(c);
            dc.SaveChanges();
            return RedirectToAction("Index");


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dc.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Comment(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event event1 = dc.Events.Find(id);

            if (event1 == null)
            {
                return HttpNotFound();
            }
            return View(event1);

        }

        public ActionResult Map(string venue, string city,int id)
        {
            
            ViewBag.map1 = venue + city;

            Event ma = dc.Events.Find(id);

            var res = (from q in dc.Maps
                where q.eventid == ma.eventid
                select q).SingleOrDefault();


            Map a = dc.Maps.Find(res.mapid);
           
            
            return View(a);
        }

    }
}
