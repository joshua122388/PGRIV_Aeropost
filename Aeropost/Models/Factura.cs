using System;
using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        public string NumeroFactura { get; set; }

        public DateTime Fecha { get; set; }

        public string CedulaCliente { get; set; }

        public decimal MontoTotal { get; set; }


    }
}