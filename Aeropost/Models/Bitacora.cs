using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Aeropost.Models
{
    public class Bitacora
    {

        /// <summary>
        /// Identificador único del registro de bitácora - Clave primaria autoincremental en la base de datos
        /// </summary>
        private int id;

        /// <summary>
        /// Nombre de usuario (login) que inició sesión en el sistema
        /// </summary>
        private string user;

        /// <summary>
        /// Nombre completo del usuario que inició sesión (se muestra en la interfaz)
        /// </summary>
        private string nombreCompleto;

        /// <summary>
        /// Fecha y hora exacta del evento de inicio de sesión
        /// </summary>
        private DateTime fechaLogin;

        /// <summary>
        /// Constructor parametrizado para crear un registro de bitácora con todos los datos
        /// </summary>
        /// <param name="id">Identificador único del registro</param>
        /// <param name="user">Nombre de usuario (login)</param>
        /// <param name="nombreCompleto">Nombre completo del usuario</param>
        /// <param name="fechaLogin">Fecha y hora del evento</param>
        public Bitacora(int id, string user, string nombreCompleto, DateTime fechaLogin)
        {
            this.id = id;
            this.user = user;
            this.nombreCompleto = nombreCompleto;
            this.fechaLogin = fechaLogin;
        }

        /// <summary>
        /// Constructor por defecto que inicializa un registro de bitácora con valores predeterminados
        /// </summary>
        public Bitacora()
        {
            this.id = 0;                 // Se asignará por la base de datos
            this.user = "";              // Se llenará al registrar el evento
            this.nombreCompleto = "";    // Se llenará al registrar el evento
            this.fechaLogin = DateTime.Now; // Momento actual del servidor
        }


        /// <summary>
        /// Propiedad pública para acceder al ID del registro de bitácora
        /// </summary>
        [Required]
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Propiedad pública para acceder al nombre de usuario (login)
        /// </summary>
        [Display(Name = "Usuario")]
        public string User { get => user; set => user = value; }

        /// <summary>
        /// Propiedad pública para acceder al nombre completo del usuario
        /// </summary>
        [Display(Name = "Nombre de Usuario")]
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }

        /// <summary>
        /// Propiedad pública para acceder a la fecha y hora del evento
        /// </summary>
        [Display(Name = "Fecha y Hora de login")]
        public DateTime FechaHora { get => fechaLogin; set => fechaLogin = value; }

    }
}