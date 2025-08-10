using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    // Representa un cliente registrado en la plataforma
    public class Cliente
    {
        // Campos privados para almacenar los datos del cliente
        private string cedula;            // Identificación única del cliente
        private string nombre;            // Nombre completo del cliente
        private string tipo;              // Tipo de cliente (Regular, Frecuente, Suspendido)
        private string correo;            // Correo electrónico del cliente
        private string direccionEntrega;  // Dirección de entrega de paquetes
        private string telefono;          // Teléfono de contacto

        // Constructor con parámetros para inicializar todos los campos
        public Cliente(string cedula, string nombre, string tipo, string correo, string direccionEntrega, string telefono)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.tipo = tipo;
            this.correo = correo;
            this.direccionEntrega = direccionEntrega;
            this.telefono = telefono;
        }

        // Constructor sin parámetros que inicializa los campos con valores por defecto
        public Cliente()
        {
            this.cedula = "";
            this.nombre = "";
            this.tipo = "";
            this.correo = "";
            this.direccionEntrega = "";
            this.telefono = "";
        }

        // Propiedades públicas con validaciones para acceder y modificar los campos privados

        [Key] // Indica que 'Cedula' es la clave primaria en la base de datos
        [Required] // El campo es obligatorio
        [StringLength(20)] // Longitud máxima permitida
        public string Cedula { get => cedula; set => cedula = value; }

        [Required] // El campo es obligatorio
        [StringLength(100)] // Longitud máxima permitida
        public string Nombre { get => nombre; set => nombre = value; }

        [Required] // El campo es obligatorio
        [StringLength(20)] // Longitud máxima permitida
        public string Tipo { get => tipo; set => tipo = value; } // Regular, Frecuente, Suspendido

        [Required] // El campo es obligatorio
        [EmailAddress] // Valida formato de correo electrónico
        public string Correo { get => correo; set => correo = value; }

        [Required] // El campo es obligatorio
        [StringLength(200)] // Longitud máxima permitida
        public string DireccionEntrega { get => direccionEntrega; set => direccionEntrega = value; }

        [Required] // El campo es obligatorio
        [Phone] // Valida formato de número telefónico
        public string Telefono { get => telefono; set => telefono = value; }
    }
}
