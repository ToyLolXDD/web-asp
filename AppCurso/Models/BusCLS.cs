using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class BusCLS
    {
        [Display(Name = "Id Bus")]
        [Key]
        public int idBus { get; set; }
        [Display(Name = "Nombre Sucursal")]
        [Required]
        public int idSucursal { get; set; }
        [Display(Name = "Tipo Bus")]
        [Required]
        public int idTipobus { get; set; }
        [Display(Name = "Modelo")]
        [Required]
        public int idModelo { get; set; }
        [Display(Name = "Placa")]
        [Required]
        public string placa { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }
        [Display(Name = "Numero Filas")]
        [Required]
        public int numeroFilas { get; set; }
        [Display(Name = "Numero Columnas")]
        [Required]
        public int numeroColumnas { get; set; }
        public int bhabilitado { get; set; }
        [Display(Name = "Descripcion")]
        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }

        [Display(Name = "Observacion")]
        [StringLength(200, ErrorMessage ="Maximo 200 caracteres")]
        [DataType(DataType.MultilineText)]
        public string observacion { get; set; }
        [Display(Name = "Nombre Marca")]
        [Required]
        public int idMarca { get; set; }

        // propiedadesadicionales
        [Display(Name = "Nombre Sucursal")]
        public string nombreSucursal { get; set; }
        [Display(Name = "Nombre Modelo")]
        public string nombreModelo { get; set; }
        public string nombreTipoBus { get; set; }

        public string mensajeError { get; set; }
    }
}