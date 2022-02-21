using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P6_P3.Models;
using P6_P3.Models.TableViewModels;

namespace P6_P3.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult MyPerfil()
        {
            Usuarios oUser = (Usuarios)Session["Users"];
            var model = new UsuariosTableViewModel
            {
                IdUser = oUser.IdUser,
                Pass = oUser.Pass,
                FirstName = oUser.FirstName,
                LastName = oUser.LastName,
                Telefono = oUser.Telefono,
                Email = oUser.Email,
                Saldo = oUser.Saldo
            };

            return View(model);
        }
    }
}