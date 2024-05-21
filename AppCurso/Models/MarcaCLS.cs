using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class MarcaCLS
    {
        [Display(Name = "Id Marca")]
        public int id_marca { get; set; }
        [Display(Name = "Nombre Marca")]
        [Required]
        [StringLength(200, ErrorMessage ="La longitud maxima es 200")]
        public string nombre { get; set; }
        [Display(Name = "Descripcion Marca")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud maxima es 200")]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }
        public string bhabilitado { get; set; }
        //Propiedad mensaja para almacenar mensajes de error 
        public string mensajeError { get; set; }
    }
}