using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    /// <summary>
    /// Representa un usuario del sistema Aeropost con toda la información necesaria
    /// para autenticación, registro y gestión de perfil de usuario
    /// </summary>
    public class Usuario
    {

        /// <summary>
        /// Identificador único del usuario - Clave primaria autoincremental en la base de datos
        /// </summary>
        private int id;

        /// <summary>
        /// Nombre completo del usuario que se mostrará en la interfaz después del login
        /// Incluye nombres y apellidos completos
        /// </summary>
        private string nombre;

        /// <summary>
        /// Número de cédula de identidad del usuario
        /// Campo inmutable una vez creado el usuario (no se puede editar al actualizar)
        /// </summary>
        private string cedula;

        /// <summary>
        /// Género del usuario - Solo acepta valores "femenino" o "masculino"
        /// Según especificación del enunciado del proyecto
        /// </summary>
        private string genero;

        /// <summary>
        /// Fecha y hora exacta cuando se registró el usuario en el sistema
        /// Se asigna automáticamente al momento de la creación
        /// </summary>
        private DateTime fechaRegistro;

        /// <summary>
        /// Estado actual del usuario en el sistema
        /// Valores permitidos: "activo" (puede usar el sistema) | "inactivo" (cuenta suspendida)
        /// </summary>
        private string estado;

        /// <summary>
        /// Nombre de usuario único utilizado para iniciar sesión en el sistema
        /// Debe ser único en toda la base de datos
        /// </summary>
        private string user;

        /// <summary>
        /// Contraseña del usuario almacenada en texto plano
        /// NOTA: Solo para propósitos de entrada, en producción debería estar encriptada
        /// </summary>
        private string pass;

        /// <summary>
        /// Constructor parametrizado para crear un usuario con todos los datos especificados
        /// Utilizado principalmente cuando se cargan datos existentes desde la base de datos
        /// </summary>
        /// <param name="id">Identificador único del usuario</param>
        /// <param name="nombre">Nombre completo del usuario</param>
        /// <param name="cedula">Número de cédula de identidad</param>
        /// <param name="genero">Género del usuario (femenino/masculino)</param>
        /// <param name="fechaRegistro">Fecha de registro en el sistema</param>
        /// <param name="estado">Estado actual del usuario (activo/inactivo)</param>
        /// <param name="user">Nombre de usuario para login</param>
        /// <param name="pass">Contraseña del usuario</param>
        public Usuario(int id, string nombre, string cedula, string genero, DateTime fechaRegistro, string estado, string user, string pass)
        {
            // Asignación directa de todos los parámetros a los campos privados correspondientes
            this.id = id;
            this.nombre = nombre;
            this.cedula = cedula;
            this.genero = genero;
            this.fechaRegistro = fechaRegistro;
            this.estado = estado;
            this.user = user;
            this.pass = pass;
        }

        /// <summary>
        /// Constructor por defecto que inicializa un usuario vacío con valores predeterminados
        /// Utilizado para crear nuevos usuarios desde formularios web
        /// </summary>
        public Usuario()
        {
            // Inicialización de campos numéricos con valor por defecto
            this.id = 0;                    // ID será asignado por la base de datos

            // Inicialización de campos de texto como cadenas vacías
            this.nombre = "";       // Se llenará desde el formulario
            this.cedula = "";              // Se llenará desde el formulario
            this.genero = "";              // Se seleccionará desde el formulario

            // Valores automáticos con lógica de negocio
            this.fechaRegistro = DateTime.Now; // Fecha actual del servidor al momento de creación
            this.estado = "activo";            // Por defecto los usuarios nuevos están activos

            // Campos de autenticación vacíos
            this.user = "";                // Se llenará desde el formulario
            this.pass = "";                // Se llenará desde el formulario
        }

        /// <summary>
        /// Propiedad pública para acceder al ID del usuario
        /// Marcada como [Required] para validación automática en formularios
        /// </summary>
        [Required]
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Propiedad pública para acceder al nombre completo del usuario
        /// Se utiliza para mostrar información del usuario en la interfaz
        /// </summary>
        public string Nombre { get => nombre; set => nombre = value; }

        /// <summary>
        /// Propiedad pública para acceder a la cédula del usuario
        /// Campo de solo lectura después de la creación inicial
        /// </summary>
        public string Cedula { get => cedula; set => cedula = value; }

        /// <summary>
        /// Propiedad pública para acceder al género del usuario
        /// Debe validarse que solo contenga "femenino" o "masculino"
        /// </summary>
        public string Genero { get => genero; set => genero = value; }

        /// <summary>
        /// Propiedad pública para acceder a la fecha de registro
        /// Generalmente de solo lectura, se asigna automáticamente
        /// </summary>
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }

        /// <summary>
        /// Propiedad pública para acceder al estado del usuario
        /// Permite activar/desactivar cuentas de usuario administrativamente
        /// </summary>
        public string Estado { get => estado; set => estado = value; }

        /// <summary>
        /// Propiedad pública para acceder al nombre de usuario
        /// Debe ser único en el sistema para evitar conflictos de autenticación
        /// </summary>
        public string User { get => user; set => user = value; }

        /// <summary>
        /// Propiedad pública para acceder a la contraseña
        /// En un sistema de producción debería manejarse con mayor seguridad
        /// </summary>
        public string Pass { get => pass; set => pass = value; }
    }
}