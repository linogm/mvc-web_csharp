using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P6_P3.Models.TableViewModels
{
    public class AnunciosTableViewModel
    {
        public int IdAnuncio { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public float Horas { get; set; }
        public int Usuario1 { get; set; }
        public string UsuarioName { get; set; }
        public int? Estado { get; set; }

    }
}