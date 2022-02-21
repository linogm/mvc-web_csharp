using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace P6_P3.Models.TableViewModels
{
    public class UsuariosTableViewModel
    {
        [Display(Name ="ID de Usuario")]
        public int IdUser { get; set; }
        [Display(Name = "Password")]
        public string Pass { get; set; }
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
        [Display(Name = "Telefono")]
        public int Telefono { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Saldo de horas")]
        public float Saldo { get; set; }
    }
}