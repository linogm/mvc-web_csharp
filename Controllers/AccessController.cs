using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P6_P3.Models;

namespace P6_P3.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        //Obtener ID Usuario Session
        //public void setId(int Id)
        //{
        //    using (p6dbEntities db = new p6dbEntities())
        //    {
        //        oSesion oSess = new oSesion();
        //        oSess.Valor = Id;
        //        db.oSesion.Add(oSess);
        //        db.SaveChanges();
        //    }
        //}
        
        public ActionResult Enter(string user, string password)
        {
            try
            {
                using (p6dbEntities db = new p6dbEntities())
                {
                    var lst = from d in db.Usuarios
                              where d.Email == user && d.Pass == password
                              select d;
                    if (lst.Count() > 0)
                    {
                        Usuarios oUser = lst.First();
                        Session["Users"] = oUser;
                        //enviamos ID a getId()
                        //setId(lst.First().IdUser);
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario inválido :( ");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error :( " + ex.Message);
            }
        }

    }
}