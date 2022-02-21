using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P6_P3.Models;

namespace P6_P3.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Logout()
        {
            //Reseteamos el valor SesionId
            //using (p6dbEntities db = new p6dbEntities())
            //{
            //    var list = db.oSesion.ToList();
            //    db.oSesion.RemoveRange(list);
            //    db.SaveChanges();
            //}

            Session["Users"] = null;
            return RedirectToAction("Index", "Access");
        }
    }
}