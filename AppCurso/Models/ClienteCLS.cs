using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class ClienteCLS
    {
        [Required]
        [Display(Name = "Id Cliente")]

        public int idcliente { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "La longitud maxima es 100")]
        [Display(Name = "Nombre Cliente")]
        public string nombre { get; set; }
        [StringLength(100, ErrorMessage = "La longitud maxima es 100")]
        [Required]
        [Display(Name = "Apellido Materno Cliente")]
        public string apmaterno { get; set; }
        [StringLength(100, ErrorMessage = "La longitud maxima es 100")]
        [Required]
        [Display(Name = "Apellido Paterno Cliente")]
        public string appaterno { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Ingrese un correo valido")]
        [Display(Name = "Email Cliente")]
        public string email { get; set; }
        [StringLength(200, ErrorMessage = "La longitud maxima es 200")]
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Direccion Cliente")]
        public string direccion { get; set; }
        [Required]
        [Display(Name = "Sexo")]
        public int? idsexo { get; set; }
        [Required]
        [Display(Name = "Telefono Fijo Cliente")]
        [StringLength(10, ErrorMessage = "La longitud maxima es 10")]
        public string telefonoFijo { get; set; }
        [StringLength(10, ErrorMessage = "La longitud maxima es 10")]
        [Required]
        [Display(Name = "Telefono Celular Cliente")]
        public string telefonoCelular { get; set; }
        [Required]
        public int bhabilitado { get; set; }
        
        public int btieneusuario { get; set; }
        
        public string tipousuario { get; set; }

        //propiedad adicional
        public string mensajeError { get; set; }
    }
}