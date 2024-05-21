using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class PaginaCLS
    {
        [Display(Name = "Id Pagina")]
        public int iidPagina { get; set; }
        [Display(Name = "Titulo del Link")]
        [Required]
        public string mensaje { get; set; }
        [Display(Name = "Nombre de la accion")]
        [Required]
        public string accion { get; set; }
        [Display(Name = "Nombre del controlador")]
        [Required]
        public string controlador { get; set; }
        public int bhabilitado { get; set; }

    }
}