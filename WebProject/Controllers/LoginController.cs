using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    
    public class LoginController : Controller
    {
        EventdbEntities2 dc = new EventdbEntities2();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Admin admin)
        {
            var res = (from q in dc.Admins
                where q.adminname == admin.adminname && q.adminpassword == admin.adminpassword
                select q).SingleOrDefault();

            if (res == null)
            {
                return View("Index");
            } else
            {
                Session["id"] = res.adminid;
                Session["adminname"] = res.adminname;
                return RedirectToAction("Index", "Panel");
            }
        }

        public ActionResult Logout(Models.Admin admin)
        {
            
            
                Session["id"] = null;
                Session["adminname"] = null;
                return RedirectToAction("Index", "Login");
            
        }
    }
}