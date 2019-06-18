using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class PanelController : Controller
    {
        EventdbEntities2 dc = new EventdbEntities2();
        // GET: Panel
        public ActionResult Index()
        {
            var res = (from q in dc.Users
                select q).Count();
            ViewBag.TotalUsers = res;

            return View(dc.Users.ToList());
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = dc.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
                User user = dc.Users.Find(id);
                dc.Users.Remove(user);
                dc.SaveChanges();
                return RedirectToAction("Index");
            
            
        }

        public ActionResult UserEvents(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = dc.Users.Find(id);

            var res = from q in dc.Events
                where q.userid == id
                select q;
            
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(res.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}