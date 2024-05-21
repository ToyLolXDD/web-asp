using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class EmpleadoCLS
    {
        public int id_empleado { get; set; }
        
        [StringLength(100, ErrorMessage = "La longitud maxima es 100")]
        [Required]
        [Display(Name = "Nombre Empleado")]
        public string nombre { get; set; }
        [StringLength(100, ErrorMessage = "La longitud maxima es 100")]
        [Required]
        [Display(Name = "Apellido Materno")]
        public string apmaterno { get; set; }
        [StringLength(100, ErrorMessage = "La longitud maxima es 100")]
        [Required]
        [Display(Name = "Apellido Paterno")]
        public string appaterno { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaContrato { get; set; }
        [Required]
        [Display(Name = "Tipo usuario")]
        public int idtipoUsuario { get; set;  }
        [Required]
        [Display(Name = "Tipo contrato")]
        public int idtipoContrato { get; set; }
        public int idSexo { get; set; }
        public int bhabilitado { get; set; }
        [Required]
        [Range(0,100000, ErrorMessage ="Fuera de rango")]
        [Display(Name ="Sueldo")]
        public decimal sueldo { get; set; }
        public string nombreTipousuario { get; set; }
        public string nombreTipocontrato { get; set; }

        public string mensajeError { get; set; }



    }
}