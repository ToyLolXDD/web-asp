using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class TipoUsuarioCLS
    {
        [Display(Name="Id Tipo Usuario")]
        public int iidTipoUsuario { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(150, ErrorMessage ="demasiados carateres")]
        public string nombre { get; set; }
        [Display(Name = "Descripcion")]
        [StringLength(250, ErrorMessage = "demasiados carateres")]
        [Required]
        public string descripcion { get; set; }
        
        public int bhabilitado { get; set; }
    }
}