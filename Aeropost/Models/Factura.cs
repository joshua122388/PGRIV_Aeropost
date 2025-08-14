using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aeropost.Models
{
    public class Factura
    {
        // Campos privados para almacenar los datos de la factura
        private int id;
        private string numeroFactura;
        private string numeroTracking;
        private string cedulaCliente;
        private DateTime fechaEntrega;
        private decimal montoTotal;

        // Constructor con parámetros para inicializar todos los campos


        public Factura(int id, string numeroFactura, string numeroTracking, string cedulaCliente, DateTime fechaEntrega, decimal montoTotal)
        {
            this.id = id;
            this.numeroFactura = numeroFactura;
            this.numeroTracking = numeroTracking;
            this.cedulaCliente = cedulaCliente;
            this.fechaEntrega = fechaEntrega;
            this.montoTotal = montoTotal;
        }

        // Constructor sin parámetros que inicializa los campos con valores por defecto
        public Factura()
        {
            this.numeroFactura = "";
            this.numeroTracking = "";
            this.cedulaCliente = "";
            this.fechaEntrega = DateTime.Now;
            this.montoTotal = 0;
        }

        // metodo para calcular el monto total de la factura
        public decimal calcularMontoFactura(decimal peso, decimal valorTotal, bool productoEspecial)
        {
            const decimal tarifaBase = 12m;
            decimal impuestos = valorTotal * 0.13m;
            decimal cargosAdicionales = productoEspecial ? valorTotal * 0.10m : 0m;
            return (peso * tarifaBase) + impuestos + cargosAdicionales;
        }

        // Propiedades públicas con validaciones
        [Required]
        public int Id { get => id; set => id = value; }
        public string NumeroFactura { get => numeroFactura; set => numeroFactura = value; }
        public string NumeroTracking { get => numeroTracking; set => numeroTracking = value; }
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }
        public DateTime FechaEntrega { get => fechaEntrega; set => fechaEntrega = value; }
        public decimal MontoTotal { get => montoTotal; set => montoTotal = value; }

    }
}