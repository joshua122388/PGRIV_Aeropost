using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aeropost.Models
{
    /// <summary>
    /// Representa una factura generada por la entrega de un paquete en el sistema Aeropost
    /// Contiene toda la información necesaria para el control financiero y la trazabilidad
    /// de las entregas realizadas a los clientes
    /// </summary>
    public class Factura
    {
        /// <summary>
        /// Identificador único de la factura - Clave primaria autoincremental en la base de datos
        /// Se genera automáticamente al insertar una nueva factura
        /// </summary>
        private int id;

        /// <summary>
        /// Número único de factura para identificación fiscal y contable
        /// Debe seguir un formato específico y ser único en todo el sistema
        /// Se utiliza para referencias externas y reportes contables
        /// </summary>
        private string numeroFactura;

        /// <summary>
        /// Número de tracking del paquete asociado a esta factura
        /// Establece la relación directa entre la factura y el paquete entregado
        /// Permite trazabilidad completa desde el registro hasta la facturación
        /// </summary>
        private string numeroTracking;

        /// <summary>
        /// Cédula de identidad del cliente que recibe la factura
        /// Establece la relación con la entidad Cliente para identificar al receptor
        /// Se utiliza para generar reportes por cliente y historial de facturas
        /// </summary>
        private string cedulaCliente;

        /// <summary>
        /// Fecha exacta en que se realizó la entrega del paquete y se generó la factura
        /// Fundamental para reportes de facturación por períodos y control de entregas
        /// </summary>
        private DateTime fechaEntrega;

        /// <summary>
        /// Monto total facturado al cliente incluyendo todos los cargos aplicables
        /// Incluye: tarifa base por peso + impuestos + cargos adicionales por productos especiales
        /// </summary>
        private decimal montoTotal;

        /// <summary>
        /// Constructor parametrizado para crear una factura con todos los datos especificados
        /// Utilizado principalmente cuando se cargan facturas existentes desde la base de datos
        /// o cuando se crea una factura con datos completos desde el sistema
        /// </summary>
        /// <param name="id">Identificador único de la factura</param>
        /// <param name="numeroFactura">Número único de factura</param>
        /// <param name="numeroTracking">Número de tracking del paquete asociado</param>
        /// <param name="cedulaCliente">Cédula del cliente receptor</param>
        /// <param name="fechaEntrega">Fecha de entrega y facturación</param>
        /// <param name="montoTotal">Monto total a facturar</param>
        public Factura(int id, string numeroFactura, string numeroTracking, string cedulaCliente, DateTime fechaEntrega, decimal montoTotal)
        {
            // Asignación directa de todos los parámetros a los campos privados correspondientes
            this.id = id;
            this.numeroFactura = numeroFactura;
            this.numeroTracking = numeroTracking;
            this.cedulaCliente = cedulaCliente;
            this.fechaEntrega = fechaEntrega;
            this.montoTotal = montoTotal;
        }

        /// <summary>
        /// Constructor por defecto que inicializa una factura vacía con valores predeterminados
        /// Utilizado para crear nuevas facturas desde formularios web o procesos automáticos
        /// Los valores se completarán posteriormente según la lógica de negocio
        /// </summary>
        public Factura()
        {
            // Inicialización de campos de texto como cadenas vacías
            this.numeroFactura = "";        // Se generará automáticamente o desde formulario
            this.numeroTracking = "";       // Se obtendrá del paquete a facturar
            this.cedulaCliente = "";        // Se obtendrá del cliente asociado al paquete

            // Valores automáticos con lógica de negocio
            this.fechaEntrega = DateTime.Now;   // Fecha actual del servidor al momento de creación
            this.montoTotal = 0;                // Se calculará usando el método calcularMontoFactura
        }

        /// <summary>
        /// Método para calcular automáticamente el monto total de una factura
        /// Aplica la lógica de negocio de Aeropost para determinar el costo final
        /// basado en el peso del paquete, valor declarado y tipo de productos
        /// </summary>
        /// <param name="peso">Peso del paquete en kilogramos</param>
        /// <param name="valorTotal">Valor total declarado de los productos en el paquete</param>
        /// <param name="productoEspecial">Indica si el paquete contiene productos especiales</param>
        /// <returns>Monto total calculado que debe facturarse al cliente</returns>
        public decimal calcularMontoFactura(decimal peso, decimal valorTotal, bool productoEspecial)
        {
            // Tarifa base por kilogramo según política de precios de Aeropost
            const decimal tarifaBase = 12m;

            // Cálculo de impuestos - 13% sobre el valor declarado de los productos
            // Este porcentaje corresponde a impuestos de importación y manejo
            decimal impuestos = valorTotal * 0.13m;

            // Cálculo de cargos adicionales - 10% extra solo si contiene productos especiales
            // Los productos especiales requieren manejo especializado y mayor seguro
            decimal cargosAdicionales = productoEspecial ? valorTotal * 0.10m : 0m;

            // Retorna el cálculo final: (peso × tarifa base) + impuestos + cargos adicionales
            // Esta es la fórmula completa de facturación establecida por la empresa
            return (peso * tarifaBase) + impuestos + cargosAdicionales;
        }

        public static decimal ObtenerFacturacionTotalPorMes(List<Factura> facturas, int mes, int anio)
        {
            return facturas
                .Where(f => f.FechaEntrega.Month == mes && f.FechaEntrega.Year == anio)
                .Sum(f => f.MontoTotal);
        }

        /// <summary>
        /// Obtiene todas las facturas asociadas a una cédula específica
        /// </summary>
        /// <param name="facturas">Lista de facturas a filtrar</param>
        /// <param name="cedula">Cédula del cliente</param>
        /// <returns>Lista de facturas del cliente especificado</returns>
        public static List<Factura> ObtenerFacturasPorCedula(List<Factura> facturas, string cedula)
        {
            return facturas.Where(f => f.CedulaCliente == cedula).ToList();
        }

        /// <summary>
        /// Propiedad pública para acceder al ID de la factura
        /// Marcada como [Required] para validación automática en formularios
        /// Clave primaria que identifica únicamente cada factura en el sistema
        /// </summary>
        [Required]
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Propiedad pública para acceder al número de factura
        /// Campo obligatorio que debe ser único en todo el sistema
        /// Se utiliza para referencias fiscales y búsquedas rápidas
        /// </summary>
        public string NumeroFactura { get => numeroFactura; set => numeroFactura = value; }

        /// <summary>
        /// Propiedad pública para acceder al número de tracking
        /// Establece la relación con la entidad Paquete a través de su número de tracking
        /// Permite trazar el origen del paquete que generó esta factura
        /// </summary>
        public string NumeroTracking { get => numeroTracking; set => numeroTracking = value; }

        /// <summary>
        /// Propiedad pública para acceder a la cédula del cliente
        /// Establece la relación con la entidad Cliente a través de su cédula
        /// Fundamental para generar reportes de facturación por cliente
        /// </summary>
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }

        /// <summary>
        /// Propiedad pública para acceder a la fecha de entrega
        /// Registra el momento exacto cuando se completó la entrega y se generó la factura
        /// Crítica para reportes de facturación por períodos y métricas de tiempo
        /// </summary>
        public DateTime FechaEntrega { get => fechaEntrega; set => fechaEntrega = value; }

        /// <summary>
        /// Propiedad pública para acceder al monto total de la factura
        /// Contiene el valor final que debe pagar el cliente
        /// Se calcula automáticamente o se puede asignar manualmente según el proceso
        /// </summary>
        public decimal MontoTotal { get => montoTotal; set => montoTotal = value; }
    }

     public void CalcularMonto()
        {
            const decimal TARIFA_BASE = 12m;
            decimal baseCargo = Peso * TARIFA_BASE;
            decimal impuesto13 = ValorTotalPaquete * 0.13m;
            decimal adicional10 = EsEspecial ? ValorTotalPaquete * 0.10m : 0m;
            MontoPagar = baseCargo + impuesto13 + adicional10;
        }
    }