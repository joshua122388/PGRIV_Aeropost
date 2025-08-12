using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aeropost.Models
{
    public class Paquete
    {
        [Key]
        public int Id { get; set; }

       
        public string CedulaCliente { get; set; }

      
        public Cliente Cliente { get; set; } 

     
        public string TiendaOrigen { get; set; } // resellers como  Amazon, Shein, Temu que le hacen servicio a aeropost

        [Required, Range(0.01, 999999)]
        public decimal Peso { get; set; }

        [Required, Range(0.01, 9999999)]
        [Display(Name = "Valor total ($)")]
        public decimal ValorTotal { get; set; }

       
        public bool ProductosEspeciales { get; set; }

       
        public string NumeroTracking { get; set; } // se genera al crear un paquete 

        [Required]
        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
