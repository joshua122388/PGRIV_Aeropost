using System;
using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Número de Factura")]
        public string NumeroFactura { get; set; }

        [Required]
        [Display(Name = "Número de Tracking")]
        public string NumeroTracking { get; set; } // Relación con Paquete

        [Required]
        [Display(Name = "Cédula del Cliente")]
        public string CedulaCliente { get; set; } // Relación con Cliente

        [Required]
        [Display(Name = "Fecha de Entrega")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }

        [Required]
        [Display(Name = "Monto Total")]
        public decimal MontoTotal { get; set; }
        
    }
}