using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aeropost.Models
{
    public class Paquete
    {
        // Campos privados para almacenar los datos del paquete
        private int id;                     // Identificador único del paquete
        private string cedulaCliente;       // Cédula del cliente propietario del paquete
        private string tiendaOrigen;        // Tienda donde se originó el paquete (Amazon, Shein, etc.)
        private decimal peso;               // Peso del paquete en libras o kilogramos
        private decimal valorTotal;         // Valor total del contenido del paquete
        private bool productosEspeciales;   // Indica si contiene productos especiales
        private string numeroTracking;      // Número de seguimiento generado automáticamente
        private DateTime fechaRegistro;     // Fecha y hora de registro del paquete

        // Constructor con parámetros para inicializar todos los campos
        public Paquete(int id, string cedulaCliente, string tiendaOrigen, decimal peso, decimal valorTotal, bool productosEspeciales, string numeroTracking, DateTime fechaRegistro)
        {
            this.id = id;
            this.cedulaCliente = cedulaCliente;
            this.tiendaOrigen = tiendaOrigen;
            this.peso = peso;
            this.valorTotal = valorTotal;
            this.productosEspeciales = productosEspeciales;
            this.numeroTracking = numeroTracking;
            this.fechaRegistro = fechaRegistro;
        }

        // Constructor sin parámetros que inicializa los campos con valores por defecto
        public Paquete()
        {
            this.id = 0;                        // ID será asignado por la base de datos
            this.cedulaCliente = "";            // Se llenará desde el formulario
            this.tiendaOrigen = "";             // Se llenará desde el formulario
            this.peso = 0.0m;                   // Peso inicial en cero
            this.valorTotal = 0.0m;             // Valor inicial en cero
            this.productosEspeciales = false;   // Por defecto no contiene productos especiales
            this.numeroTracking = "";           // Se generará automáticamente
            this.fechaRegistro = DateTime.Now;  // Fecha actual del servidor
        }

        // Propiedades públicas con validaciones para acceder y modificar los campos privados

        [Key] // Indica que 'Id' es la clave primaria en la base de datos
        public int Id { get => id; set => id = value; }

        [Required] // El campo es obligatorio
        [StringLength(20)] // Longitud máxima permitida
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }

        // Propiedad de navegación para Entity Framework
        public Cliente Cliente { get; set; }

        [Required] // El campo es obligatorio
        [StringLength(100)] // Longitud máxima permitida
        public string TiendaOrigen { get => tiendaOrigen; set => tiendaOrigen = value; }

        [Required] // El campo es obligatorio
        [Range(0.01, 999999)] // Rango de valores permitidos
        public decimal Peso { get => peso; set => peso = value; }

        [Required] // El campo es obligatorio
        [Range(0.01, 9999999)] // Rango de valores permitidos
        [Display(Name = "Valor total ($)")] // Etiqueta para mostrar en formularios
        public decimal ValorTotal { get => valorTotal; set => valorTotal = value; }

        public bool ProductosEspeciales { get => productosEspeciales; set => productosEspeciales = value; }

        [StringLength(50)] // Longitud máxima permitida
        public string NumeroTracking { get => numeroTracking; set => numeroTracking = value; }

        [Required] // El campo es obligatorio
        [Display(Name = "Fecha de registro")] // Etiqueta para mostrar en formularios
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}