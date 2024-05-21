using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class RolCLS
    {
        [Display(Name = "Id Rol")]
        public int iidRol { get; set; }
        [Required]
        [Display(Name = "Nombre Rol")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Descripcion Rol")]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }
    }
}