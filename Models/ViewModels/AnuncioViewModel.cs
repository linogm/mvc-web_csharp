using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace P6_P3.Models.ViewModels
{
    public class AnuncioViewModel
    {
        //public int IdAnuncio { get; set; }
        //[Required]
        //[Display(Name = "Fecha de publicación")]
        //public System.DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Descripción del anuncio")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Número de horas")]
        public float Horas { get; set; }

        //[Required]
        //[Display(Name = "Introduce tu ID de usuario")]
        //public int Usuario1 { get; set; }
        
    }

    public class EditAnuncioViewModel
    {
        public int IdAnuncio { get; set; }
        
        //[Required]
        //[Display(Name = "Fecha de publicación")]
        //public System.DateTime Fecha { get; set; }

        [Display(Name = "Descripción del anuncio")]
        public string Descripcion { get; set; }

        [Display(Name = "Número de horas")]
        public float Horas { get; set; }

        //[Required]
        //[Display(Name = "Introduce tu ID de usuario")]
        //public int Usuario1 { get; set; }

    }
}