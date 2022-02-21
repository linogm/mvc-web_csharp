using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P6_P3.Models;
using P6_P3.Models.ViewModels;
using P6_P3.Models.TableViewModels;

namespace P6_P3.Controllers
{
    public class ComprasController : Controller
    {
        // GET: MyCompras
        
        public ActionResult MyCompras()
        {
            Usuarios usuario = new Usuarios();
            usuario = (Usuarios)Session["Users"];
            int IdSesion = usuario.IdUser;

            List<AnunciosTableViewModel> lst = null;
            using (p6dbEntities db = new p6dbEntities())

            {
                lst = (from d in db.Anuncios
                       where d.Usuario2 == IdSesion
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
    }

}