using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCurso.Models
{
    public class ViajeCLS
    {
        [Display(Name = "Id Viaje")]
        [Required]
        public int idViaje { get; set; }
        [Display(Name = "Origen")]
        [Required]
        public int idLugarOrigen { get; set; }
        [Display(Name = "Destino")]
        [Required]
        public int idLugarDestino { get; set; }
        [Display(Name = "Precio")]
        [Required]
        public double precio { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha viaje")]
        [Required]
        public DateTime fechaViaje { get; set; }
        [Required]
        [Display(Name = "Id Bus")]
        public int idBus { get; set; }
        [Display(Name = "Asientos Disponibles")]
        [Required]
        public int numeroAsientosDis { get; set; }
        public string placa { get; set; }

        //PropADIC
        [Display(Name = "Origen")]
        public string nombreLugarOrigen { get; set; }
        [Display(Name = "Destino")]
       
        public string nombreLugarDestino { get; set; }
        [Display(Name = "Bus")]
      
        public string nombreBus { get; set; }
    }
}