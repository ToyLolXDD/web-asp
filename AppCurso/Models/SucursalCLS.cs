using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class SucursalCLS
    {
        [Display(Name ="Id Sucursal")]
        public int id_sucursal { get; set; }
        [Display(Name = "Nombre Sucursal")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud maxima es 200")]
        public string nombre { get; set; }
        [Display(Name = "Direccion Sucursal")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud maxima es 200")]
        public string direccion { get; set; }
        [Display(Name = "Telefono Sucursal")]
        [Required]
        [StringLength(10, ErrorMessage = "La longitud maxima es 10")]
        public string telefono { get; set; }
        [Display(Name = "Email Sucursal")]
        [EmailAddress(ErrorMessage ="Ingrese un correo valido")]
        [Required]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Display(Name = "Fecha Apertura")]
        public DateTime fechaApertura { get; set; }
        public int bhabilitado { get; set; }
        
        //PROPIDEEADA QADIONCAL
        public string mensajeError { get; set; }
    }
}