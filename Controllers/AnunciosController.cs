using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P6_P3.Models;
using P6_P3.Models.TableViewModels;
using P6_P3.Models.ViewModels;

namespace P6_P3.Controllers
{
    public class AnunciosController : Controller
    {
        //public int giveId()
        //{
        //    p6dbEntities db = new p6dbEntities();
        //    var item = (from d in db.oSesion
        //                   where d.IdSesion == 1
        //                   select new { d.Valor }).Single();

        //    int IdSesion = item.Valor;
        //    return IdSesion;
        //}

        public int giveId()
        {
            Usuarios user = new Usuarios();
            user = (Usuarios)Session["Users"];
            return user.IdUser;
        }

        // GET: Anuncios
        public ActionResult Index()
        {
            Usuarios usuario = new Usuarios();
            usuario = (Usuarios)Session["Users"];
            int IdSesion = usuario.IdUser; 
            
            List<AnunciosTableViewModel> lst = null;
            using (p6dbEntities db = new p6dbEntities())
            {
                lst = (from d in db.Anuncios
                       where d.Usuario1 != IdSesion && d.Estado==1
                       //where d.Estado==1
                       orderby d.Fecha
                       join e in db.Usuarios
                       on d.Usuario1 equals e.IdUser
                       select new AnunciosTableViewModel
                       {
                           IdAnuncio = d.IdAnuncio,
                           Fecha = d.Fecha,
                           Descripcion = d.Descripcion,
                           Horas = d.Horas,
                           Usuario1 = d.Usuario1,
                           UsuarioName = e.FirstName,
                           Estado = d.Estado,
                       }).ToList();
            }

            return View(lst);
        }
        
        //GET Mis Anuncios
        public ActionResult MyIndex()
        {
            Usuarios usuario = new Usuarios();
            usuario = (Usuarios)Session["Users"];
            int IdSesion = usuario.IdUser; 
            
            List<AnunciosTableViewModel> lst = null;
            using (p6dbEntities db = new p6dbEntities())
            
            {
                lst = (from d in db.Anuncios
                       where (d.Usuario1 == IdSesion && d.Estado==1)
                       orderby d.Fecha
                       join e in db.Usuarios
                       on d.Usuario1 equals e.IdUser
                       select new AnunciosTableViewModel
                       {
                           IdAnuncio = d.IdAnuncio,
                           Fecha = d.Fecha,
                           Descripcion = d.Descripcion,
                           Horas = d.Horas,
                           Usuario1 = d.Usuario1,
                           UsuarioName = e.FirstName,
                           Estado = d.Estado,
                       }).ToList();
            }

            return View(lst);
        }

        // CRUD Agregar Nuevo anuncio
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AnuncioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db=new p6dbEntities())
            {
                Anuncios oAnuncio = new Anuncios();
                oAnuncio.Fecha = DateTime.Today;
                oAnuncio.Descripcion = model.Descripcion;
                oAnuncio.Horas = model.Horas;
                //oAnuncio.Usuario1 = model.Usuario1;
                oAnuncio.Usuario1 = giveId();
                oAnuncio.Estado = 1;
                
                db.Anuncios.Add(oAnuncio);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Anuncios/MyIndex"));
        }

        // CRUD: Editar Anuncios
        public ActionResult Edit(int Id)
        {
            EditAnuncioViewModel model = new EditAnuncioViewModel();

            using (var db = new p6dbEntities())
            {
                var oAnuncio = db.Anuncios.Find(Id);
                model.Descripcion = oAnuncio.Descripcion;
                model.Horas = oAnuncio.Horas;
                model.IdAnuncio = oAnuncio.IdAnuncio;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditAnuncioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new p6dbEntities())
            {
                var oAnuncio = db.Anuncios.Find(model.IdAnuncio);
                oAnuncio.Descripcion = model.Descripcion;
                oAnuncio.Horas = model.Horas;

                //if (model.Password!=null)

                db.Entry(oAnuncio).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/Anuncios/MyIndex"));

        }

        // CRUD: Eliminar 
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db=new p6dbEntities())
            {
                var oAnuncio = db.Anuncios.Find(Id);

                //db.Anuncios.Remove(oAnuncio); //Connected Scenario?
                db.Entry(oAnuncio).State = System.Data.Entity.EntityState.Deleted; //Disconnected scenario?
                db.SaveChanges();
            }

            return Content("1");
        }

        // GET: MyCompras
        //public ActionResult Compras()
        //{
        //    Usuarios usuario = new Usuarios();
        //    usuario = (Usuarios)Session["Users"];
        //    int IdSesion = usuario.IdUser;

        //    List<AnunciosTableViewModel> lst = null;
        //    using (p6dbEntities db = new p6dbEntities())

        //    {
        //        lst = (from d in db.Anuncios
        //               where d.Usuario2 == IdSesion
        //               orderby d.Fecha
        //               select new AnunciosTableViewModel
        //               {
        //                   IdAnuncio = d.IdAnuncio,
        //                   Fecha = d.Fecha,
        //                   Descripcion = d.Descripcion,
        //                   Horas = d.Horas,
        //                   Usuario1 = d.Usuario1,
        //                   Estado = d.Estado,
        //               }).ToList();
        //    }

        //    return View(lst);
        //}

        // COMPRAS Solicitar Anuncio
        [HttpPost]
        public ActionResult Comprar(int Id)
        {
            using (var db = new p6dbEntities())
            {
                var oAnuncio = db.Anuncios.Find(Id);
                Usuarios oUser = new Usuarios();
                Usuarios oUser2 = new Usuarios();
                oUser = (Usuarios)Session["Users"];
                oUser2 = db.Usuarios.Find(oAnuncio.Usuario1);
                if (oUser.Saldo >= oAnuncio.Horas)
                {
                    oAnuncio.Usuario2 = oUser.IdUser;
                    oAnuncio.Estado = 2;

                    oUser.Saldo = oUser.Saldo - oAnuncio.Horas;
                    oUser2.Saldo = oUser2.Saldo + oAnuncio.Horas;

                    db.Entry(oAnuncio).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(oUser2).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Content("1");
                } else {
                    //return Content("Saldo insuficiente :( ");
                    return Content("2");
                }

            }

            //return Content("2");
        }
        [HttpPost]
        public double Saldo()
        {
            using (var db = new p6dbEntities())
            {
                Usuarios oUser = new Usuarios();
                oUser = (Usuarios)Session["Users"];
                //string Saldo = toString(oUser.Saldo);
                return oUser.Saldo;
            }

            //return Content("2");
        }

    }
}