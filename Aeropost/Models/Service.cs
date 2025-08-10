using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;  // Importa Entity Framework para trabajar con base de datos

namespace Aeropost.Models
{
    /// <summary>
    /// Clase de servicio que actúa como contexto de base de datos y capa de acceso a datos
    /// Hereda de DbContext para proporcionar funcionalidad ORM (Object-Relational Mapping)
    /// </summary>
    public class Service : DbContext
    {
        /// <summary>
        /// Representa la tabla "Usuario" en la base de datos
        /// DbSet permite realizar operaciones CRUD (Create, Read, Update, Delete) sobre los usuarios
        /// </summary>
        public DbSet<Usuario> usuarios { get; set; }

        /// <summary>
        /// Representa la tabla "Clientes" en la base de datos
        /// DbSet permite realizar operaciones CRUD sobre los clientes
        /// </summary>
        public DbSet<Clientes> clientes { get; set; }

        /// <summary>
        /// Representa la tabla "Bitacora" en la base de datos
        /// DbSet permite realizar operaciones CRUD sobre los registros de bitácora
        /// </summary>
        public DbSet<Bitacora> bitacora { get; set; }

        /// <summary>
        /// Constructor que inicializa el contexto de base de datos
        /// Llama al constructor base pasando "Aeropost" como nombre de la cadena de conexión
        /// Esta cadena esta definida en appsettings.json
        /// </summary>
        public Service() : base("Aeropost") { }

        // Región que agrupa todos los métodos relacionados con operaciones de Usuario
        #region Metodos Usuario

        /// <summary>
        /// Método para agregar un nuevo usuario a la base de datos
        /// Realiza la inserción y confirma los cambios inmediatamente
        /// </summary>
        /// <param name="usuario">Objeto Usuario con todos los datos a insertar</param>
        public void agregarUsuario(Usuario usuario)
        {
            // Agrega el usuario al contexto (estado: Added)
            usuarios.Add(usuario);

            // Ejecuta la instrucción SQL INSERT y confirma los cambios en la BD
            SaveChanges();
        }

        /// <summary>
        /// Método para obtener todos los usuarios de la base de datos
        /// Convierte el DbSet en un Array para facilitar su manipulación
        /// </summary>
        /// <returns>Array con todos los usuarios registrados</returns>
        public Array mostrarUsuario()
        {
            // Convierte el DbSet<Usuario> en un Array
            // Esto ejecuta una consulta SELECT * FROM Usuario
            return usuarios.ToArray();
        }

        /// <summary>
        /// Método para buscar un usuario específico por su ID
        /// Lanza excepción si el usuario no existe
        /// </summary>
        /// <param name="id">ID único del usuario a buscar</param>
        /// <returns>Usuario encontrado</returns>
        /// <exception cref="Exception">Se lanza cuando el usuario no existe</exception>
        public Usuario buscarUsuario(int id)
        {
            // Busca el primer usuario que coincida con el ID proporcionado
            // FirstOrDefault retorna null si no encuentra ninguna coincidencia
            var usuarioBuscado = this.usuarios.FirstOrDefault(x => x.Id == id);

            // Verifica si se encontró el usuario
            if (usuarioBuscado != null)
                return usuarioBuscado;  // Retorna el usuario encontrado
            else
                // Lanza excepción personalizada si no se encuentra el usuario
                // IMPORTANTE: En Razor Pages, esta excepción debe ser capturada
                // en el code-behind para mostrar un mensaje amigable al usuario
                throw new Exception("Usuario no encontrado");
        }

        /// <summary>
        /// Método para eliminar un usuario de la base de datos
        /// NOTA: El nombre del método no coincide con la funcionalidad (dice "Estudiante" pero elimina "Usuario")
        /// </summary>
        /// <param name="usuario">Objeto Usuario a eliminar (debe existir en el contexto)</param>
        public void eliminarUsuario(Usuario usuario)
        {
            // Marca el usuario para eliminación en el contexto
            this.usuarios.Remove(usuario);

            // Ejecuta la instrucción SQL DELETE y confirma los cambios
            SaveChanges();
        }

        /// <summary>
        /// Método para actualizar un usuario existente en la base de datos
        /// Busca el usuario por ID y actualiza todas sus propiedades editables
        /// </summary>
        /// <param name="usuario">Usuario con los nuevos datos a actualizar</param>
        /// <exception cref="Exception">Se lanza cuando el usuario a actualizar no existe</exception>
        public void actualizarUsuario(Usuario usuario)
        {
            // Busca el usuario existente en la base de datos por ID
            // Este usuario está "tracked" por Entity Framework
            var usuarioAntiguo = usuarios.FirstOrDefault(x => x.Id == usuario.Id);

            // Verifica si el usuario existe antes de intentar actualizarlo
            if (usuarioAntiguo != null)
            {
                // Actualiza cada propiedad del usuario existente con los nuevos valores
                // Entity Framework detecta automáticamente estos cambios (Change Tracking)

                usuarioAntiguo.Nombre = usuario.Nombre;  // Actualiza nombre completo
                usuarioAntiguo.Cedula = usuario.Cedula;                  // Permite corrección de cédula
                usuarioAntiguo.Genero = usuario.Genero;                  // Actualiza género
                usuarioAntiguo.Estado = usuario.Estado;                  // Permite activar/desactivar usuario
                usuarioAntiguo.User = usuario.User;                      // Actualiza nombre de usuario
                usuarioAntiguo.Pass = usuario.Pass;                      // Actualiza contraseña

                // IMPORTANTE: FechaRegistro NO se actualiza intencionalmente
                // Debe conservarse la fecha original de cuando se creó el usuario
                // Esto mantiene la integridad histórica de los datos

                // Ejecuta la instrucción SQL UPDATE con solo los campos modificados
                SaveChanges();
            }
            else
            {
                // Lanza excepción si el usuario a actualizar no existe
                // En Razor Pages, esto debe manejarse en el code-behind para:
                // 1. Mostrar mensaje de error al usuario
                // 2. Redirigir a página de listado
                // 3. Registrar el error en logs si es necesario
                throw new Exception("Usuario no encontrado");
            }

        }
        // funcion para validar el login de un usuario
        public Usuario login(string user, string password)
        {
            var usuarioLogueado = usuarios.FirstOrDefault(x => x.User == user && x.Pass == password);
            if (usuarioLogueado != null)
            { 
                return usuarioLogueado;  // Retorna el usuario si las credenciales son correctas
            }
            else throw new Exception("Usuario o contraseña incorrectos");
        }
        #endregion
    }
}