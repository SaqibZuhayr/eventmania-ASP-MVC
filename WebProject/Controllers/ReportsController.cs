using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ReportsController : Controller
    {
        private EventdbEntities2 db = new EventdbEntities2();

        // GET: Reports
        public ActionResult Index()
        {
            var reports = db.Reports.Include(r => r.Event);
            return View(reports.ToList());
        }

        
       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);

            var res = (from q in db.Events
                where q.eventid == report.eventid
                select q.eventid).SingleOrDefault();

            Event ev = db.Events.Find(res);

                var res1 = (from q in db.Maps
                where q.eventid == ev.eventid
                select q.mapid).SingleOrDefault();

            var res2 = (from q in db.Comments
                where q.eventid == ev.eventid
                select q.commentid).SingleOrDefault();

            Comment c = db.Comments.Find(res2);

            
            




            Map a = db.Maps.Find(res1);
            db.Maps.Remove(a);
            db.Comments.Remove(c);
            db.Reports.Remove(report);
            db.Events.Remove(ev);
           
           
            

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
